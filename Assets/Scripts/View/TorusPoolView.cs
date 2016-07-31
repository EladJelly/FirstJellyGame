using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class TorusPoolView : MonoBehaviour
    {
        [SerializeField]
        private TorusView _pinkTorus;
        [SerializeField]
        private TorusView _yellowTorus;
        [SerializeField]
        private TorusView _redTorus;

        private GameObject _specialTorus;
        private List<float> _spawnSpotsList;

        void Awake()
        {
            GameObjectPoolingManager.Instance.CreateGameObjectPool(_pinkTorus.Name, _pinkTorus.gameObject, 3, gameObject);
            GameObjectPoolingManager.Instance.CreateGameObjectPool(_yellowTorus.Name, _yellowTorus.gameObject, 3, gameObject);
            GameObjectPoolingManager.Instance.CreateGameObjectPool(_redTorus.Name, _redTorus.gameObject, 1, gameObject);
        }

        void OnEnable()
        {
            GameEventsController.LoadLevelEvent += LoadLevelElements;
            GameEventsController.BonusEventStarted += ReleaseSpecialTorus;
            GameEventsController.BonusEventEnded += RemoveSpecialTorus;
            GameEventsController.LoadLevelEvent += RemoveSpecialTorus;
        }

        void OnDisable()
        {
            GameEventsController.LoadLevelEvent -= LoadLevelElements;
            GameEventsController.BonusEventStarted -= ReleaseSpecialTorus;
            GameEventsController.BonusEventEnded -= RemoveSpecialTorus;
            GameEventsController.LoadLevelEvent -= RemoveSpecialTorus;
        }

        private void LoadLevelElements()
        {			
            var currentLevelData = LevelsConfigurationData.Levels[GameSessionData.CurrentLevel - 1];
            _spawnSpotsList = new List<float>();
            foreach (var torusData in currentLevelData.Toruses)
            {
                int currentTorusCount = torusData.Value;
                for (int i = 0; i < currentTorusCount; i++)
                {					
                    var element = GameObjectPoolingManager.Instance.GetObject(torusData.Key);
					element.transform.position = new Vector3(GameObjectPoolingManager.Instance.GetAvailableSpawnPoint(), 0, 0);
                    element.SetActive(true);
                }
            }
        }

        private float GetAvailableSpawnSpot()
        {
            bool spotFound = false;
            float spotValue = 0f;
            while (!spotFound)
            {
                spotValue = Random.Range(GameConfigurationData.TorusSpawnRangeMin, GameConfigurationData.TorusSpawnRangeMax);
                spotFound = !IsSpotTaken(spotValue);
            }
            _spawnSpotsList.Add(spotValue);
            return spotValue;
        }

        private bool IsSpotTaken(float spotValue)
        {
            foreach (var takenSpot in _spawnSpotsList)
            {
                if (takenSpot > spotValue - 1 && takenSpot < spotValue + 1)
                {
                    return true;
                }
            }
            return false;
        }

        private void ReleaseSpecialTorus()
        {
            _specialTorus = GameObjectPoolingManager.Instance.GetObject(_redTorus.Name);
			_specialTorus.transform.position = new Vector3(GameObjectPoolingManager.Instance.GetAvailableSpawnPoint(), 0, 0);
            _specialTorus.SetActive(true);
        }

        private void RemoveSpecialTorus()
        {
            if (_specialTorus == null || !_specialTorus.activeSelf) return;

            GameObjectPoolingManager.Instance.ReleaseObject(_redTorus.Name, _specialTorus);
        }
    }
}
