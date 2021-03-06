﻿namespace Assets.Scripts.Model
{
    public static class GameElementsModel
    {
        public enum ElementColor
        {
            Pink,
            Yellow,
            Red
        }

        public enum TargetReached
        {
            None,
            DifferentType,
            SameType,
            Special
        }

        public enum ElementName
        {
            PinkTorus,
            YellowTorus,
            RedTorus,
            PinkTarget,
            YellowTarget
        }
    }
}
