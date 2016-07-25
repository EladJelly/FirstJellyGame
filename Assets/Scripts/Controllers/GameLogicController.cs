using System.Collections;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    public class GameLogicController : MonoBehaviour
    {
        void Awake()
        {
            Physics.gravity = new Vector3(0, GameConfigurationData.Gravity, 0);
        }

        void Start()
        {
            Time.timeScale = 1f;
            GameSessionData.ResetData();
            GameSessionData.TimeLeft = GameConfigurationData.GameDurationSeconds;
            StartCoroutine(TimerCountdown());
        }

        void OnEnable()
        {
            GameEventsController.TorusEnterEvent += IncreaseScore;
            GameEventsController.TorusExitEvent += DecreaseScore;
        }

        void OnDisable()
        {
            GameEventsController.TorusEnterEvent -= IncreaseScore;
            GameEventsController.TorusExitEvent -= DecreaseScore;
        }

        private void IncreaseScore(bool isSameColor)
        {
            int points = isSameColor ? GameConfigurationData.TargetReachedSameColorPoints : GameConfigurationData.TargetReachedPoints;
            GameSessionData.Score += points;
            GameEventsController.OnScoreUpdate(points);
            GameSessionData.TorusCollectedCount++;
            CheckGameProgress();
        }

        private void DecreaseScore(bool isSameColor)
        {
            int points = isSameColor ? GameConfigurationData.TargetReachedSameColorPoints : GameConfigurationData.TargetReachedPoints;
            GameSessionData.Score -= points;
            GameEventsController.OnScoreUpdate(-points);
            GameSessionData.TorusCollectedCount--;
        }

        private IEnumerator TimerCountdown()
        {
            while (GameSessionData.TimeLeft != 0)
            {
                yield return new WaitForSeconds(1f);
                GameSessionData.TimeLeft--;
            }
            Time.timeScale = 0f;
            GameEventsController.OnGameOver();
        }

        private void CheckGameProgress()
        {
            if (GameSessionData.TorusCollectedCount == GameConfigurationData.TorusCount)
            {
                Time.timeScale = 0f;
                GameEventsController.OnGameCompleted();
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
