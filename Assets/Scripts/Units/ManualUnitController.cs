using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.AttributeEvents.Assets.Runtime.GameEvents;
using Hudossay.BuggyBattle.Assets.Scripts.Input;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class ManualUnitController : MonoBehaviour
    {
        [EventLocal(UnitMovementEvents.NewMovementVector)] public GameEvent<Vector2> NewMovingVector;
        [EventLocal(UnitMovementEvents.NewAimPoint)] public GameEvent<Vector3> NewAimPoint;

        public Transform PlayerCamera;
        public float MaxRaycastDistance;


        private void Update()
        {
            var cameraDirection = PlayerCamera.transform.forward;

            var aimPoint = Physics.Raycast(PlayerCamera.position, cameraDirection, out var raycastHit, MaxRaycastDistance)
                ? raycastHit.point
                : cameraDirection * MaxRaycastDistance + PlayerCamera.position;

            NewAimPoint.Raise(aimPoint);
        }


        [ResponseGlobal(InputEvent.Movement)]
        public void OrderNewMovingVector(Vector2 vector) =>
            NewMovingVector.Raise(vector);


        private void Reset() =>
            MaxRaycastDistance = 100;
    }
}
