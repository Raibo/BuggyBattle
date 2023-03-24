using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class UnitDrivingMotionController : MonoBehaviour
    {
        public Rigidbody Body;

        [Header("Moving")]
        public float MaxForwardSpeed;
        [Tooltip("Expected to be negative")]
        public float MaxBackwardSpeed;
        public float MaxAcceleration;

        [Header("Turning")]
        public float MaxRotationSpeed;
        public float MaxRotationAcceleration;

        private Vector2 _motionVector;
        private Transform _bodyTransform;


        private void Awake() =>
            _bodyTransform = Body.gameObject.transform;


        private void FixedUpdate()
        {
            Accelerate();
            Rotate();
        }


        [ResponseLocal(UnitMovementEvents.NewMovementVector)]
        public void OnNewMovementVector(Vector2 vector) =>
            _motionVector = vector;


        private void Accelerate()
        {
            var accelerationSignal = _motionVector.y;
            var currentSpeed = _bodyTransform.InverseTransformDirection(Body.velocity).z;

            if (accelerationSignal == 0)
                return;

            if (accelerationSignal < 0 && currentSpeed < accelerationSignal * -MaxBackwardSpeed)
                return;

            if (accelerationSignal > 0 && currentSpeed > accelerationSignal * MaxForwardSpeed)
                return;

            var forwardDesiredAcceleration = accelerationSignal * MaxAcceleration;
            var acceleration = _bodyTransform.TransformDirection(new Vector3(0, 0, forwardDesiredAcceleration));
            Body.AddForce(acceleration, ForceMode.Acceleration);
        }


        private void Rotate()
        {
            var rotationSignal = _motionVector.x;
            var currentRotationSpeed = Body.angularVelocity.y;

            if (rotationSignal == 0)
                return;

            if (rotationSignal < 0 && currentRotationSpeed < rotationSignal * MaxRotationSpeed)
                return;

            if (rotationSignal > 0 && currentRotationSpeed > rotationSignal * MaxRotationSpeed)
                return;

            Body.AddRelativeTorque(0, rotationSignal * MaxRotationAcceleration, 0, ForceMode.Acceleration);
        }
    }
}
