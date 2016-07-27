using Assets.Scripts.Controllers;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class LevelView : MonoBehaviour
    {
        private Text _text;
        void Awake()
        {
            _text = GetComponent<Text>();
        }

        void OnEnable()
        {
            GameEventsController.LoadLevelEvent += UpdateLevelView;
        }

        void OnDisable()
        {
            GameEventsController.LoadLevelEvent -= UpdateLevelView;
        }

        private void UpdateLevelView()
        {
            _text.text = "LVL " + GameSessionData.CurrentLevel;
        }
        
    }
}
