using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Pooling;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public Transform FirePoint;
        public int WeaponIndex;
        public float RateOfFire;
        public bool IsFiring;

        private Pool _projectilePool;
        private float _reloadTime;
        private float _sinceReload;


        private void OnEnable()
        {
            _reloadTime = 1f/RateOfFire;
            _projectilePool = PoolsCarrier.Instance.GetPoolForPrefab(ProjectilePrefab);
        }


        private void FixedUpdate()
        {
            _sinceReload += Time.deltaTime;

            if (!IsFiring)
                return;

            if (_sinceReload < _reloadTime)
                return;

            Fire();
        }


        [ResponseLocal(UnitControlEvents.OpenFire)]
        public void OnOpenFire(int index)
        {
            if (index != WeaponIndex)
                return;

            IsFiring = true;
        }


        [ResponseLocal(UnitControlEvents.StopFire)]
        public void OnStopFire(int index)
        {
            if (index != WeaponIndex)
                return;

            IsFiring = false;
        }


        private void Fire()
        {
            _sinceReload = 0f;
            var projectile = _projectilePool.Rent().GameObject;
            projectile.SetActive(true);
            projectile.transform.SetPositionAndRotation(FirePoint.position, FirePoint.rotation);
        }
    }
}
