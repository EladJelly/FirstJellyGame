using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class LevelsConfigurationData
    {
        private static List<LevelData> _levels;
        public static List<LevelData> Levels
        {
            get
            {
                if (_levels == null)
                {
                    _levels = new List<LevelData>()
                    {
                        //LVL 1
                        new LevelData()
                        {
                            Toruses = new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.PinkTorus, 1 }
                            },
                            Targets =  new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.PinkTarget, 1 }
                            }
                        },
						//LVL 2
						new LevelData()
                        {
                            Toruses = new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.YellowTorus, 2 }
                            },
                            Targets =  new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.YellowTarget, 2 }
                            }
                        },
                         //LVL 3
                        new LevelData()
                        {
                            Toruses = new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.PinkTorus, 1 },
                                { GameElementsModel.ElementName.YellowTorus, 1 }
                            },
                            Targets =  new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.PinkTarget, 1 },
                                { GameElementsModel.ElementName.YellowTarget, 1 }
                            }
                        },
                         //LVL 4
                        new LevelData()
                        {
                            Toruses = new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.PinkTorus, 2 },
                                { GameElementsModel.ElementName.YellowTorus, 2 }
                            },
                            Targets =  new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.YellowTarget, 1 },
                                { GameElementsModel.ElementName.PinkTarget, 1 }
                            }
                        },
                         //LVL 5
                        new LevelData()
                        {
                            Toruses = new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.PinkTorus, 3 },
                                { GameElementsModel.ElementName.YellowTorus, 3 }
                            },
                            Targets =  new Dictionary<GameElementsModel.ElementName, int>
                            {
                                { GameElementsModel.ElementName.PinkTarget, 1 },
                                { GameElementsModel.ElementName.YellowTarget, 1 }
                            }
                        }
                    };

                }
                return _levels;
            }
        }
    }
}
