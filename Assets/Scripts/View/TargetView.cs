using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class TargetView : MonoBehaviour
    {
        public GameElementsModel.ElementColor Color;

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
            TargetId = (int) Color;
        }
    }
}
