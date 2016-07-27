using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class GameObjectPool
    {
        //Object prefab for pooling
        private GameObject _pooledObject;
        //Number of pre-instantiated objects
        private int _initialPoolSize;    
        //The list of objects
        private List<GameObject> _pooledObjectsList;

        public GameObjectPool(GameObject objectPrefab, int initialPoolSize, GameObject parent)
        {
            _pooledObjectsList = new List<GameObject>();
            _initialPoolSize = initialPoolSize;
            _pooledObject = objectPrefab;

            //Instantiate the pooled objects and hold them in a list
            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject obj = GameObject.Instantiate(_pooledObject);
                obj.transform.SetParent(parent.transform);
                obj.SetActive(false);
                _pooledObjectsList.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < _pooledObjectsList.Count; i++)
            {
                //Pool an inactive object (available for pooling)
                if (!_pooledObjectsList[i].activeInHierarchy)
                {
                    return _pooledObjectsList[i];
                }
            }
            //Not enough pooled objects, need to add more to the list
            GameObject newObj = GameObject.Instantiate(_pooledObject);
            _pooledObjectsList.Add(newObj);
            return newObj;
        }

        public void ReleasePooledObject(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
        }

        //Deactivate all objects
        public void ResetPool()
        {
            if (_pooledObjectsList.Count == 0) return;
            for (int i = 0; i < _pooledObjectsList.Count; i++)
            {
                _pooledObjectsList[i].SetActive(false);
            }
        }

    }
}