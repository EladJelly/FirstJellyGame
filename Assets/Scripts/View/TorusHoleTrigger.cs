using System;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class TorusHoleTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider otherObject)
        {
            try
            {
                GameEventsController.OnTorusEntered(IsSameElementColor(otherObject));
            }
            catch (Exception)
            {
            }
            
        }

        void OnTriggerExit(Collider otherObject)
        {
            try
            {
                GameEventsController.OnTorusExit(IsSameElementColor(otherObject));
            }
            catch (Exception)
            {
            }
        }

        private bool IsSameElementColor(Collider otherObject)
        {
            var torus = gameObject.GetComponentInParent<TorusView>();
            var target = otherObject.GetComponentInParent<TargetView>();

            if (!torus || !target)
            {
                throw new Exception();
            }

            return torus.Color == target.Color;
        }
    }
}
