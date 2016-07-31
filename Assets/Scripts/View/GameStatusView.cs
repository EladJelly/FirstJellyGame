using System;
using Assets.Scripts.Controllers;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class GameStatusView : MonoBehaviour
    {
        private Text _text;
        [SerializeField]
        private GameObject _restartButton;

        private float _bonusTimeLeft;

        void Awake()
        {
            _text = GetComponent<Text>();
        }

        void OnEnable()
        {
            GameEventsController.GameOverEvent += ShowGameOver;
            GameEventsController.GameCompletedEvent += ShowGameCompleted;
            GameEventsController.BonusEventStarted += ShowBonusCountdownTimer;
            GameEventsController.BonusEventEnded += HideBonusCountdownTimer;
            GameEventsController.LoadLevelEvent += HideBonusCountdownTimer;
        }

        void OnDisable()
        {
            GameEventsController.GameOverEvent -= ShowGameOver;
            GameEventsController.GameCompletedEvent -= ShowGameCompleted;
            GameEventsController.BonusEventStarted -= ShowBonusCountdownTimer;
            GameEventsController.BonusEventEnded -= HideBonusCountdownTimer;
            GameEventsController.LoadLevelEvent -= HideBonusCountdownTimer;
        }

        private void ShowGameOver()
        {
            _text.text = "GAME OVER";
            _text.color = new Color32(203, 0, 0, 255);
            _restartButton.SetActive(true);
        }

        private void ShowGameCompleted()
        {
            _text.text = "YOU WON!";
            _text.color = new Color32(1, 118, 14, 255);
            _restartButton.SetActive(true);
        }

        private void ShowBonusCountdownTimer()
        {
            _text.color = new Color32(255, 0, 0, 255);
            _bonusTimeLeft = GameConfigurationData.SpecialTorusDuration;
        }

        private void HideBonusCountdownTimer()
        {
            _bonusTimeLeft = 0;
            _text.text = String.Empty;
        }

        void Update()
        {
            if (_bonusTimeLeft > 0)
            {
                _text.text = string.Format("{0:0}", _bonusTimeLeft%60);
                _bonusTimeLeft -= Time.deltaTime;
            }
        }

		public void OnRestart()
		{
			_text.text = String.Empty;
			GameEventsController.OnRestartGame();
		}
    }
}
