using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class LevelData
    {
        public Dictionary<GameElementsModel.ElementName, int> Targets;
        public Dictionary<GameElementsModel.ElementName, int> Toruses;

        public int GetTorusCount()
        {
            int totalTorusCount = 0;
            foreach (var torusData in Toruses)
            {
                totalTorusCount += torusData.Value;
            }
            return totalTorusCount;
        }

        public int GetTargetCount()
        {
            int totalTargetCount = 0;
            foreach (var targetData in Targets)
            {
                totalTargetCount += targetData.Value;
            }
            return totalTargetCount;
        }
    }
}