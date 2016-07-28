using System.Collections;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.View;

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
            LoadLevel(1);
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
			if (GameSessionData.CurrentState != GameSessionData.GameState.LevelStarted) return;

            UpdateScore(type, true);
            GameSessionData.TorusCollectedCount++;
            CheckGameProgress();
        }

        private void DecreaseScore(GameElementsModel.TargetReached type)
        {
			if (GameSessionData.CurrentState != GameSessionData.GameState.LevelStarted) return;

            UpdateScore(type, false);
            GameSessionData.TorusCollectedCount--;
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
				Debug.Log (GameSessionData.TimeLeft);
                //if (GameSessionData.TimeLeft % GameConfigurationData.SpecialTorusFrequency == 0)
                //{
                //    GameEventsController.OnBonusEventStarted();
                //    StartCoroutine(StartBonusTimerCountdown());
                //}
            }
			LoadLevel (GameSessionData.CurrentLevel);
        }

        private IEnumerator StartBonusTimerCountdown()
        {
            yield return new WaitForSeconds(10f);
            GameEventsController.OnBonusEventEnded();
        }

        private void CheckGameProgress()
        {
			int currentTorusCount = LevelsConfigurationData.Levels[GameSessionData.CurrentLevel - 1].GetTorusCount();
            if (GameSessionData.TorusCollectedCount == currentTorusCount)
            {
				GameSessionData.CurrentState = GameSessionData.GameState.LevelCompleted;
                if (GameSessionData.CurrentLevel == LevelsConfigurationData.Levels.Count)
                {
                    GameEventsController.OnGameCompleted();
                }
                else
                {
                    LoadLevel(GameSessionData.CurrentLevel + 1);
                }
            }
        }

        private void LoadLevel(int level)
        {
			StopCoroutine(TimerCountdown());
            GameSessionData.CurrentLevel = level;
			GameObjectPoolingManager.Instance.ReleaseAll();
            GameSessionData.ResetData();
            GameEventsController.OnLoadLevel();
            StartCoroutine(TimerCountdown());
			GameSessionData.CurrentState = GameSessionData.GameState.LevelStarted;
        }

        public void RestartGame()
        {
            LoadLevel(1);
        }
    }
}
