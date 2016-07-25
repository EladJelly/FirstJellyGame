using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public static class GameEventsController
    {
        public static event Action<Vector3> ClickAreaEvent;
        public static void OnClickAreaTriggered(Vector3 hitPoint)
        {
            if (ClickAreaEvent != null)
            {
                ClickAreaEvent(hitPoint);
            }
        }

        public static event Action<bool> TorusEnterEvent;
        public static void OnTorusEntered(bool isSameColor)
        {
            if (TorusEnterEvent != null)
            {
                TorusEnterEvent(isSameColor);
            }
        }

        public static event Action<bool> TorusExitEvent;
        public static void OnTorusExit(bool isSameColor)
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

        public static event Action GameCompetedEvent;
        public static void OnGameCompleted()
        {
            if (GameCompetedEvent != null)
            {
                GameCompetedEvent();
            }
        }
    }
}
