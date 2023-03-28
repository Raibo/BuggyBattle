using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
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


        [ResponseLocal(UnitMovementEvents.NewAimPoint)]
        public void OnNewAimPoint(Vector3 aimPoint)
        {
            Rotate(Turret, TurretReference, aimPoint, Vector3.up);
            Rotate(WeaponMount, WeaponMountReference, aimPoint, Vector3.right);
        }


        private void Rotate(Transform rotated, Transform reference, Vector3 aimPoint, Vector3 rotationPlane)
        {
            var localAimPoint = reference.InverseTransformPoint(aimPoint);
            localAimPoint = Vector3.ProjectOnPlane(localAimPoint, rotationPlane);

            var endRotation = Quaternion.LookRotation(localAimPoint);
            var limitedRotation = Quaternion.RotateTowards(rotated.localRotation, endRotation, TurretSpeed * Time.deltaTime);
            rotated.localRotation = limitedRotation;
        }


        private void Reset() =>
            TurretSpeed = 10f;
    }
}
