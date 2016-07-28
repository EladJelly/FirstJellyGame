using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Model;
using Assets.Scripts.View;

public class TargetPoolView : MonoBehaviour
{
    [SerializeField]
    private GameObject _pinkTarget;
    [SerializeField]
    private GameObject _yellowTarget;

    void Awake()
    {
        GameObjectPoolingManager.Instance.CreateGameObjectPool(_pinkTarget.GetComponentInChildren<TargetView>().Name, _pinkTarget.gameObject, 3, gameObject);
        GameObjectPoolingManager.Instance.CreateGameObjectPool(_yellowTarget.GetComponentInChildren<TargetView>().Name, _yellowTarget.gameObject, 3, gameObject);
    }

    void OnEnable()
    {
        GameEventsController.LoadLevelEvent += LoadLevelElements;
    }

    void OnDisable()
    {
        GameEventsController.LoadLevelEvent -= LoadLevelElements;
    }

    private void LoadLevelElements()
    {
        var currentLevelData = LevelsConfigurationData.Levels[GameSessionData.CurrentLevel - 1];
        var interval = GetTargetsPositionInterval(currentLevelData);
        int currentTargetNumber = 1;
        foreach (var targetData in currentLevelData.Targets)
        {
            int currentTargetCount = targetData.Value;
            for (int i = 0; i < currentTargetCount; i++)
            {
                var element = GameObjectPoolingManager.Instance.GetObject(targetData.Key);
                var currentPosX = GameConfigurationData.TorusSpawnRangeMin + interval * currentTargetNumber;
                var position = element.transform.position;
                position.x = currentPosX;
                element.transform.position = position;
                element.SetActive(true);
                currentTargetNumber++;
            }
        }
    }

    private float GetTargetsPositionInterval(LevelData levelData)
    {
        float sceneRange = Mathf.Abs(GameConfigurationData.TorusSpawnRangeMin) + Mathf.Abs(GameConfigurationData.TorusSpawnRangeMax);
        return sceneRange / (levelData.GetTargetCount() + 1);
    }
}
