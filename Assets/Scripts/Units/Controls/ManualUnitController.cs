using Hudossay.AttributeEvents.Assets.Runtime;
using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.AttributeEvents.Assets.Runtime.GameEvents;
using Hudossay.BuggyBattle.Assets.Scripts.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units.Controls
{
    public class ManualUnitController : MonoBehaviour
    {
        [EventLocal(UnitControlEvents.NewMovementVector)] public GameEvent<Vector2> NewMovingVector;
        [EventLocal(UnitControlEvents.NewAimPoint)] public GameEvent<Vector3> NewAimPoint;
        [EventLocal(UnitControlEvents.OpenFire)] public GameEvent<int> OpenFire;
        [EventLocal(UnitControlEvents.StopFire)] public GameEvent<int> StopFire;

        public Transform PlayerCamera;
        public float MaxRaycastDistance;

        public List<GameObject> Listeners;

        private EventLinker _eventlinker;


        private void Awake()
        {
            _eventlinker = GetComponent<EventLinker>();

            foreach (var listener in Listeners)
                _eventlinker.StartBroadcastingTo(listener);
        }


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


        [ResponseGlobal(InputEvent.Weapon1Started)]
        public void OrderOpenFire1() =>
            OpenFire.Raise(1);


        [ResponseGlobal(InputEvent.Weapon2Started)]
        public void OrderOpenFire2() =>
            OpenFire.Raise(2);


        [ResponseGlobal(InputEvent.Weapon3Started)]
        public void OrderOpenFire3() =>
            OpenFire.Raise(3);


        [ResponseGlobal(InputEvent.Weapon1Canceled)]
        public void OrderStopFire1() =>
            StopFire.Raise(1);


        [ResponseGlobal(InputEvent.Weapon2Canceled)]
        public void OrderStopFire2() =>
            StopFire.Raise(2);


        [ResponseGlobal(InputEvent.Weapon3Canceled)]
        public void OrderStopFire3() =>
            StopFire.Raise(3);


        private void Reset() =>
            MaxRaycastDistance = 100;
    }
}
