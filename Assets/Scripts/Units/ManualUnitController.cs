using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.AttributeEvents.Assets.Runtime.GameEvents;
using Hudossay.BuggyBattle.Assets.Scripts.Input;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class ManualUnitController : MonoBehaviour
    {
        [EventLocal(UnitMovementEvents.NewMovementVector)] public GameEvent<Vector2> NewMovingVector;
        [EventLocal(UnitMovementEvents.NewAimingVector)] public GameEvent<Vector2> NewAimingVector;


        [ResponseGlobal(InputEvent.Movement)]
        public void OrderNewMovingVector(Vector2 vector) =>
            NewMovingVector.Raise(vector);


        [ResponseGlobal(InputEvent.Aim)]
        public void OrderNewAimingVector(Vector2 vector) =>
            NewAimingVector.Raise(vector);
    }
}
