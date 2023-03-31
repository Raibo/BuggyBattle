using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Unils;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class UnitTurretAimController : MonoBehaviour
    {
        public float TurretSpeed;

        public Transform TurretReference;
        public Transform Turret;
        public Transform WeaponMountReference;
        public Transform WeaponMount;


        private void OnEnable()
        {
            TurretReference.localPosition = Turret.localPosition;
            TurretReference.rotation = Quaternion.identity;
            WeaponMountReference.localPosition = WeaponMount.localPosition;
            WeaponMountReference.rotation = Quaternion.identity;
        }


        [ResponseLocal(UnitControlEvents.NewAimPoint)]
        public void OnNewAimPoint(Vector3 aimPoint)
        {
            var turretRotation = Rotate(Turret, TurretReference, aimPoint, Vector3.up);
            var mountAimPoint = VectorUtils.RotateAround(aimPoint, TurretReference.position, turretRotation);
            Rotate(WeaponMount, WeaponMountReference, mountAimPoint, Vector3.right);

        }


        private Quaternion Rotate(Transform rotated, Transform reference, Vector3 aimPoint, Vector3 rotationPlane)
        {
            var localAimPoint = reference.InverseTransformPoint(aimPoint);
            var localAimPointProjected = Vector3.ProjectOnPlane(localAimPoint, rotationPlane);

            var endRotation = Quaternion.LookRotation(localAimPointProjected);
            var limitedRotation = Quaternion.RotateTowards(rotated.localRotation, endRotation, TurretSpeed * Time.deltaTime);

            var aimRotation = rotated.localRotation * Quaternion.Inverse(endRotation);

            rotated.localRotation = limitedRotation;
            return aimRotation;
        }


        private void Reset() =>
            TurretSpeed = 60f;
    }
}
