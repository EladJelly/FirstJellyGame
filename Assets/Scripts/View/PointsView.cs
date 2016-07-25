using Assets.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class PointsView : MonoBehaviour
    {
        private Text _pointsText;
        private Animator _pointsAnimator;
        void Awake()
        {
            _pointsText = GetComponent<Text>();
            _pointsAnimator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            GameEventsController.ScoreUpdateEvent += ShowPoints;
        }

        void OnDisable()
        {
            GameEventsController.ScoreUpdateEvent -= ShowPoints;
        }

        private void ShowPoints(int points)
        {
            _pointsText.text = string.Empty;
            if (points > 0)
            {
                _pointsText.text = "+" + points;
                _pointsText.color = new Color32(1, 118, 14, 255);
            }
            else
            {
                _pointsText.text = "-" + (-points);
                _pointsText.color = new Color32(203, 0, 0, 255);
            }
            _pointsAnimator.Play("Points", -1, 0f);
        }
    }
}
