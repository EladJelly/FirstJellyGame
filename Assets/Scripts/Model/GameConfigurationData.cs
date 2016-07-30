namespace Assets.Scripts.Model
{
    public static class GameConfigurationData
    {
        public const int TargetReachedPoints = 10;
        public const int TargetReachedSameColorPoints = 50;
        public const int TargetReachedSpecial = 100;
        public const int GameDurationSeconds = 300;
        public const int SpecialTorusFrequency = 30; //Seconds
        public const int SpecialTorusDuration = 10; //Seconds
        public const float TorusSpawnRangeMax = 5f;
        public const float TorusSpawnRangeMin = -5f;

        //Game physics
        public const float Gravity = -2f;
        public const float ExplosionForce = 5.0f;
        public const float ExplosionRadius = 10.0f;
    }
}
