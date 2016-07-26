using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class TorusPoolView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _specialTorus;

        void OnEnable()
        {
            GameEventsController.BonusEventStarted += ReleaseSpecialTorus;
            GameEventsController.BonusEventEnded += RemoveSpecialTorus;
        }

        void OnDisable()
        {
            GameEventsController.BonusEventStarted -= ReleaseSpecialTorus;
            GameEventsController.BonusEventEnded -= RemoveSpecialTorus;
        }

        private void ReleaseSpecialTorus()
        {
            _specialTorus.transform.position = new Vector3(0, 0, 0);
            _specialTorus.SetActive(true);
        }

        private void RemoveSpecialTorus()
        {
            _specialTorus.SetActive(false);
        }
    }
}
