using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class TimerView : MonoBehaviour
    {
        private Text _timerText;
        void Awake()
        {
            _timerText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
			if (GameSessionData.CurrentState == GameSessionData.GameState.LevelStarted)
			{
				_timerText.text = string.Format ("-{0:00}:{1:00}", GameSessionData.TimeLeft / 60, GameSessionData.TimeLeft % 60);
			}
        }
    }
}
