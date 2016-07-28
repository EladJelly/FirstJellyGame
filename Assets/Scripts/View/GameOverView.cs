using Assets.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class GameOverView : MonoBehaviour
    {
        private Text _text;
        [SerializeField]
        private GameObject _restartButton;

        void Awake()
        {
            _text = GetComponent<Text>();
        }

        void OnEnable()
        {
            GameEventsController.GameOverEvent += ShowGameOver;
            GameEventsController.GameCompletedEvent += ShowGameCompleted;
        }

        void OnDisable()
        {
            GameEventsController.GameOverEvent -= ShowGameOver;
            GameEventsController.GameCompletedEvent -= ShowGameCompleted;
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
    }
}
