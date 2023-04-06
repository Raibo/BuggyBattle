using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Utils;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class UnitTurretAimController : MonoBehaviour
    {
        public float TurretSpeed;

        public Transform Turret;
        public Transform WeaponMount;


        [ResponseLocal(UnitControlEvents.NewAimPoint)]
        public void OnNewAimPoint(Vector3 aimPoint)
        {
            var turretRotation = Rotate(Turret, aimPoint, Vector3.up);
            var mountAimPoint = VectorUtils.RotateAround(aimPoint, Turret.position, turretRotation);
            Rotate(WeaponMount, mountAimPoint, Vector3.right);
        }


        private Quaternion Rotate(Transform rotated, Vector3 aimPoint, Vector3 rotationPlane)
        {
            var localAimPoint = rotated.InverseTransformPoint(aimPoint);
            var localAimPointProjected = Vector3.ProjectOnPlane(localAimPoint, rotationPlane);

            var adjustingRotation = Quaternion.FromToRotation(Vector3.forward, localAimPointProjected);
            var endRotation = rotated.localRotation * adjustingRotation;
            var limitedEndRotation = Quaternion.RotateTowards(rotated.localRotation, endRotation, TurretSpeed * Time.deltaTime);

            var aimRotation = Quaternion.Inverse(adjustingRotation);

            rotated.localRotation = limitedEndRotation;
            return aimRotation;
        }


        private void Reset() =>
            TurretSpeed = 60f;
    }
}
