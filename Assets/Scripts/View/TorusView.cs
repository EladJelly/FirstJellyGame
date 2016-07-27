using Assets.Scripts.Controllers;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class TorusView : MonoBehaviour
    {
        public GameElementsModel.ElementName Name;
        public GameElementsModel.ElementColor Color;

        private Rigidbody _rigidBody;
        private int _targetId;
        public int TargetId
        {
            get { return _targetId; }
            set
            {
                if (value > 0)
                {
                    _targetId = value;
                }
            }
        }

        void Awake()
        {
            TargetId = (int)Color;
            _rigidBody = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            GameEventsController.ClickAreaEvent += AddExplosionForce;
        }

        void OnDisable()
        {
            GameEventsController.ClickAreaEvent -= AddExplosionForce;
        }

        void AddExplosionForce(Vector3 hitPoint)
        {
            _rigidBody.AddExplosionForce(GameConfigurationData.ExplosionForce, hitPoint, GameConfigurationData.ExplosionRadius, 0, ForceMode.Impulse);
        }
    }
}
