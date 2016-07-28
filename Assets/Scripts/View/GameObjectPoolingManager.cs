using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class GameObjectPoolingManager
    {
        private Dictionary<string, GameObjectPool> _objectPools;

        private GameObjectPoolingManager()
        {
            _objectPools = new Dictionary<string, GameObjectPool>();
        }

        private static GameObjectPoolingManager _instance;

        public static GameObjectPoolingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObjectPoolingManager();
                }

                return _instance;
            }
        }

        public void CreateGameObjectPool(GameElementsModel.ElementName name, GameObject objectPrefab, int initialPoolSize, GameObject parent)
        {
            if (_objectPools.ContainsKey(name.ToString())) return;

            GameObjectPool pool = new GameObjectPool(objectPrefab, initialPoolSize, parent);
            _objectPools.Add(name.ToString(), pool);
        }

        public GameObject GetObject(GameElementsModel.ElementName name)
        {
            return _objectPools[name.ToString()].GetPooledObject();
        }

        public void ReleaseObject(GameElementsModel.ElementName name, GameObject obj)
        {
            _objectPools[name.ToString()].ReleasePooledObject(obj);
        }

        public void ReleaseAll()
        {
			foreach (var pool in _objectPools.Values)
			{
				pool.ResetPool();
			}
        }
    }
}