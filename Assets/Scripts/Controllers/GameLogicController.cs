using System.Collections;
using Assets.Scripts.Model;
using UnityEngine;
using Assets.Scripts.View;

namespace Assets.Scripts.Controllers
{
    public class GameLogicController : MonoBehaviour
    {
		[SerializeField] private GameObject _intro;
		[SerializeField] private GameObject _gameView;
        [SerializeField] private SoundController _sound;

        private AudioSource _audioSource;

        void Awake()
        {
            Physics.gravity = new Vector3(0, GameConfigurationData.Gravity, 0);
            _audioSource = _sound.GetComponent<AudioSource>();
        }

        
        void OnEnable()
        {
			GameEventsController.StartGameEvent += StartGame;
			GameEventsController.RestartGameEvent += RestartGame;
            GameEventsController.TorusEnterEvent += IncreaseScore;
            GameEventsController.TorusExitEvent += DecreaseScore;
            GameEventsController.ClickAreaEvent += PlayClickSound;
        }

        void OnDisable()
        {
			GameEventsController.StartGameEvent -= StartGame;
			GameEventsController.RestartGameEvent -= RestartGame;
            GameEventsController.TorusEnterEvent -= IncreaseScore;
            GameEventsController.TorusExitEvent -= DecreaseScore;
            GameEventsController.ClickAreaEvent -= PlayClickSound;
        }

        private void PlayClickSound(Vector3 point)
        {
            _audioSource.PlayOneShot(_sound.Up);
        }


        private void StartGame()
		{
			_intro.SetActive(false);
			_gameView.SetActive(true);
            _audioSource.PlayOneShot(_sound.GainedPoints);
            LoadLevel(1);
			StartCoroutine(TimerCountdown());
            StartCoroutine(BonusTimer());
        }

        private void IncreaseScore(GameElementsModel.TargetReached type)
        {
			if (GameSessionData.CurrentState != GameSessionData.GameState.LevelStarted) return;

            UpdateScore(type, true);
            _audioSource.PlayOneShot(_sound.GainedPoints);
            if (type == GameElementsModel.TargetReached.Special) return;

            GameSessionData.TorusCollectedCount++;
            CheckGameProgress();
        }

        private void DecreaseScore(GameElementsModel.TargetReached type)
        {
			if (GameSessionData.CurrentState != GameSessionData.GameState.LevelStarted) return;
            _audioSource.PlayOneShot(_sound.LostPoints);
            UpdateScore(type, false);

			if (type == GameElementsModel.TargetReached.Special) return;

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
			while (true)
            {				
                yield return new WaitForSeconds(1f);
				if (GameSessionData.CurrentState != GameSessionData.GameState.LevelStarted) continue;
				if (GameSessionData.TimeLeft > 0)
				{					
					GameSessionData.TimeLeft--;
				}
				else
				{
					GameSessionData.CurrentState = GameSessionData.GameState.LevelCompleted;
					GameEventsController.OnGameOver();
				}
            }
        }

        private IEnumerator BonusTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(GameConfigurationData.SpecialTorusFrequency);
                if (GameSessionData.CurrentState != GameSessionData.GameState.LevelStarted) continue;
                GameEventsController.OnBonusEventStarted();
                yield return new WaitForSeconds(GameConfigurationData.SpecialTorusDuration);
                if (GameSessionData.CurrentState != GameSessionData.GameState.LevelStarted) continue;
                GameEventsController.OnBonusEventEnded();
            }
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
			GameSessionData.CurrentLevel = level;
			GameObjectPoolingManager.Instance.ReleaseAll();
            GameSessionData.ResetData();
            GameEventsController.OnLoadLevel();
			GameSessionData.CurrentState = GameSessionData.GameState.LevelStarted;
        }

		public void RestartGame()
		{
			LoadLevel(1);
		}
    }
}
