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

        private void IncreaseScore(GameElementsModel.TargetReached type)
        {
            UpdateScore(type, true);
            GameSessionData.TorusCollectedCount--;
        }

        private void DecreaseScore(GameElementsModel.TargetReached type)
        {
            UpdateScore(type, false);
            GameSessionData.TorusCollectedCount++;
            CheckGameProgress();
        }

        private void UpdateScore(GameElementsModel.TargetReached type, bool isPositive)
        {
            int points = 0;
            switch (type)
            {
                case GameElementsModel.TargetReached.DifferentType:
                    points = GameConfigurationData.TargetReachedPoints;
                    break;
                case GameElementsModel.TargetReached.SameType:
                    points = GameConfigurationData.TargetReachedSameColorPoints;
                    break;
                case GameElementsModel.TargetReached.Special:
                    points = GameConfigurationData.TargetReachedSpecial;
                    GameEventsController.OnBonusEventEnded();
                    break;
            }

            if (!isPositive)
            {
                points *= -1;
            }

            GameSessionData.Score += points;
            GameEventsController.OnScoreUpdate(points);
        }

        private IEnumerator TimerCountdown()
        {
            while (GameSessionData.TimeLeft != 0)
            {
                yield return new WaitForSeconds(1f);
                GameSessionData.TimeLeft--;
                if (GameSessionData.TimeLeft % GameConfigurationData.SpecialTorusFrequency == 0)
                {
                    GameEventsController.OnBonusEventStarted();
                    StartCoroutine(StartBonusTimerCountdown());
                }
            }
            Time.timeScale = 0f;
            GameEventsController.OnGameOver();
        }

        private IEnumerator StartBonusTimerCountdown()
        {
            yield return new WaitForSeconds(10f);
            GameEventsController.OnBonusEventEnded();
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
