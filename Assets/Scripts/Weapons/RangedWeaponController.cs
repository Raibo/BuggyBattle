using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Pooling;
using Hudossay.BuggyBattle.Assets.Scripts.Units.Controls;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class RangedWeaponController : MonoBehaviour
    {
#if UNITY_EDITOR
        public string WeaponName;
#endif
        public GameObject ProjectilePrefab;
        public GameObject FlashPrefab;
        public Transform FirePoint;
        public int WeaponIndex;
        public float RateOfFire;

        private Pool _projectilePool;
        private Pool _flashPool;
        private float _reloadTime;
        private float _sinceReload;
        private bool _isFiring;


        private void OnEnable()
        {
            _reloadTime = 1f / RateOfFire;
            _projectilePool = PoolsCarrier.Instance.GetPoolForPrefab(ProjectilePrefab);
            _flashPool = PoolsCarrier.Instance.GetPoolForPrefab(FlashPrefab);

            _sinceReload = _reloadTime;
        }


        private void FixedUpdate()
        {
            _sinceReload += Time.deltaTime;

            if (_isFiring && _sinceReload >= _reloadTime)
                Fire();
        }


        [ResponseLocal(UnitControlEvents.OpenFire)]
        public void OnOpenFire(int index)
        {
            if (index != WeaponIndex)
                return;

            _isFiring = true;
        }


        [ResponseLocal(UnitControlEvents.StopFire)]
        public void OnStopFire(int index)
        {
            if (index != WeaponIndex)
                return;

            _isFiring = false;
        }


        private void Fire()
        {
            _sinceReload = 0f;

            RentFromPool(_flashPool);
            RentFromPool(_projectilePool);
        }


        private void RentFromPool(Pool pool)
        {
            var obj = pool.Rent().GameObject;
            obj.transform.SetPositionAndRotation(FirePoint.position, FirePoint.rotation);
            obj.SetActive(true);
        }
    }
}
