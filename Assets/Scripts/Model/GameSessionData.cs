namespace Assets.Scripts.Model
{
    public static class GameSessionData
    {
        public static int Score;
        public static int TimeLeft;
        public static int TorusCollectedCount;
        public static int CurrentLevel;

        public static void ResetData()
        {
            Score = 0;
            TimeLeft = GameConfigurationData.GameDurationSeconds;
            TorusCollectedCount = 0;
        }
    }
}
