using System;
using Assets.Scripts.Controllers;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class TorusHoleTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider otherObject)
        {
            var result = GetCollisionResult(otherObject);
            if (result != GameElementsModel.TargetReached.None)
            {
                GameEventsController.OnTorusEntered(result);
            }
        }

        void OnTriggerExit(Collider otherObject)
        {
            var result = GetCollisionResult(otherObject);
            if (result != GameElementsModel.TargetReached.None)
            {
				GameEventsController.OnTorusExit(result);
            }
        }

        private GameElementsModel.TargetReached GetCollisionResult(Collider otherObject)
        {
            var torus = gameObject.GetComponentInParent<TorusView>();
            var target = otherObject.GetComponentInParent<TargetView>();

            if (!torus || !target)
            {
                return GameElementsModel.TargetReached.None;
            }

            if (torus.Color == GameElementsModel.ElementColor.Red)
            {
                return GameElementsModel.TargetReached.Special;
            }

            if (torus.Color == target.Color)
            {
                return GameElementsModel.TargetReached.SameType;
            }

            return GameElementsModel.TargetReached.DifferentType;
        }
    }
}
