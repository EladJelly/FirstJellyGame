using System;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public static class GameEventsController
    {
        public static event Action LoadLevelEvent;
        public static void OnLoadLevel()
        {
            if (LoadLevelEvent != null)
            {
                LoadLevelEvent();
            }
        }		       

        public static event Action<Vector3> ClickAreaEvent;
        public static void OnClickAreaTriggered(Vector3 hitPoint)
        {
            if (ClickAreaEvent != null)
            {
                ClickAreaEvent(hitPoint);
            }
        }

        public static event Action<GameElementsModel.TargetReached> TorusEnterEvent;
        public static void OnTorusEntered(GameElementsModel.TargetReached isSameColor)
        {
            if (TorusEnterEvent != null)
            {
                TorusEnterEvent(isSameColor);
            }
        }

        public static event Action<GameElementsModel.TargetReached> TorusExitEvent;
        public static void OnTorusExit(GameElementsModel.TargetReached isSameColor)
        {
            if (TorusExitEvent != null)
            {
                TorusExitEvent(isSameColor);
            }
        }

        public static event Action<int> ScoreUpdateEvent;
        public static void OnScoreUpdate(int points)
        {
            if (ScoreUpdateEvent != null)
            {
                ScoreUpdateEvent(points);
            }
        }

        public static event Action GameOverEvent;
        public static void OnGameOver()
        {
            if (GameOverEvent != null)
            {
                GameOverEvent();
            }
        }

        public static event Action GameCompletedEvent;
        public static void OnGameCompleted()
        {
            if (GameCompletedEvent != null)
            {
                GameCompletedEvent();
            }
        }

        public static event Action BonusEventStarted;
        public static void OnBonusEventStarted()
        {
            if (BonusEventStarted != null)
            {
                BonusEventStarted();
            }
        }

        public static event Action BonusEventEnded;
        public static void OnBonusEventEnded()
        {
            if (BonusEventEnded != null)
            {
                BonusEventEnded();
            }
        }

		public static event Action StartGameEvent;
		public static void OnStartGame()
		{
			if (StartGameEvent != null)
			{
				StartGameEvent();
			}
		}

    }
}
