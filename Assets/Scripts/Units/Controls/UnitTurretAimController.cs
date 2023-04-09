using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Utils;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units.Controls
{
    public class UnitTurretAimController : MonoBehaviour
    {
        public float TurretSpeed;

        public Transform Turret;
        public Transform WeaponMount;


        [ResponseLocal(UnitControlEvents.NewAimPoint)]
        public void OnNewAimPoint(Vector3 aimPoint)
        {
            var turretEndRotation = RotationUtils.RotationToPointByAxis(Turret, aimPoint, Vector3.up);
            var turretLimitedRotation = Quaternion.RotateTowards(Turret.localRotation, turretEndRotation, TurretSpeed * Time.deltaTime);

            var weaponAim = RotationUtils.RotateAround(aimPoint, Turret.position, Turret.localRotation * Quaternion.Inverse(turretEndRotation));

            var weaponEndRotation = RotationUtils.RotationToPointByAxis(WeaponMount, weaponAim, Vector3.right);
            var weaponLimitedRotation = Quaternion.RotateTowards(WeaponMount.localRotation, weaponEndRotation, TurretSpeed * Time.deltaTime);

            Turret.localRotation = turretLimitedRotation;
            WeaponMount.localRotation = weaponLimitedRotation;
        }


        private void Reset() =>
            TurretSpeed = 60f;
    }
}
