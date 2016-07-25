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
        }

        void OnDisable()
        {
            GameEventsController.ScoreUpdateEvent -= UpdateScoreView;
        }

        private void UpdateScoreView(int points)
        {
            _scoreText.text = GameSessionData.Score + " pts";
        }
        
    }
}
