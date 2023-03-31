using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Pooling;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units
{
    public class RangedWeaponController : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public GameObject FlashPrefab;
        public Transform FirePoint;
        public AudioSource AudioSource;
        public AudioClip ShotSound;
        public int WeaponIndex;
        public float RateOfFire;
        public bool IsFiring;

        private Pool _projectilePool;
        private Pool _flashPool;
        private float _reloadTime;
        private float _sinceReload;


        private void OnEnable()
        {
            _reloadTime = 1f / RateOfFire;
            _projectilePool = PoolsCarrier.Instance.GetPoolForPrefab(ProjectilePrefab);
            _flashPool = PoolsCarrier.Instance.GetPoolForPrefab(FlashPrefab);

            _sinceReload = _reloadTime;

            if (AudioSource != null)
                AudioSource.clip = ShotSound;
        }


        private void FixedUpdate()
        {
            _sinceReload += Time.deltaTime;

            if (IsFiring && _sinceReload >= _reloadTime)
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

            RentFromPool(_flashPool);
            RentFromPool(_projectilePool);

            PlayShotSound();
        }


        private void RentFromPool(Pool pool)
        {
            var obj = pool.Rent().GameObject;
            obj.transform.SetPositionAndRotation(FirePoint.position, FirePoint.rotation);
            obj.SetActive(true);
        }


        private void PlayShotSound()
        {
            if (AudioSource == null || ShotSound == null)
                return;

            AudioSource.Play();
        }
    }
}
