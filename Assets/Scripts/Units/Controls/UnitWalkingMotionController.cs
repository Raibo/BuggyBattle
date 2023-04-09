using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Utils;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units.Controls
{
    public class UnitWalkingMotionController : MonoBehaviour
    {
        public Rigidbody Body;

        [Header("Walking")]
        public float MaxSpeed;
        public float MaxAcceleration;

        [Header("Turning")]
        public float MaxRotationSpeed;
        public float MaxRotationAcceleration;

        private Transform _bodyTransform;
        private Vector2 _motionVector;
        private Vector3 _aimVector;


        private void Awake() =>
            _bodyTransform = Body.gameObject.transform;


        private void FixedUpdate()
        {
            Accelerate();
            Rotate();
        }


        [ResponseLocal(UnitControlEvents.NewMovementVector)]
        public void OnNewMovementVector(Vector2 vector) =>
            _motionVector = vector;


        [ResponseLocal(UnitControlEvents.NewAimPoint)]
        public void OnNewAimVector(Vector3 vector) =>
            _aimVector = vector;


        private void Accelerate()
        {
            var acceleration = _bodyTransform.TransformDirection(new Vector3(_motionVector.x, 0, _motionVector.y));

            if (Vector3.Dot(acceleration, Body.velocity) > 0.95f && Body.velocity.sqrMagnitude >= MaxSpeed * MaxSpeed)
                return;

            Body.AddForce(acceleration * MaxAcceleration, ForceMode.Acceleration);
        }


        private void Rotate()
        {
            var endRotation = RotationUtils.RotationToPointByAxis(_bodyTransform, _aimVector, Vector3.up);
            var limitedRotation = Quaternion.RotateTowards(_bodyTransform.localRotation, endRotation, MaxRotationSpeed * Time.deltaTime);

            _bodyTransform.localRotation = limitedRotation;
        }
    }
}
