using Assets.Scripts.Controllers;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class ScoreView : MonoBehaviour
    {
        private Text _scoreText;
        void Awake()
        {
            _scoreText = GetComponent<Text>();
        }

        void OnEnable()
        {
            GameEventsController.ScoreUpdateEvent += UpdateScoreView;
			GameEventsController.LoadLevelEvent += ResetScore;
        }

        void OnDisable()
        {
            GameEventsController.ScoreUpdateEvent -= UpdateScoreView;
			GameEventsController.LoadLevelEvent -= ResetScore;
        }

        private void UpdateScoreView(int points)
        {
            _scoreText.text = GameSessionData.Score + " pts";
        }

		private void ResetScore()
		{
			_scoreText.text = GameSessionData.Score + " pts";
		}
        
    }
}
