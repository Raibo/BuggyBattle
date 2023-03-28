using Hudossay.BuggyBattle.Assets.Scripts.Pooling;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        public GameObject HitEffectPrefab;
        public Poolable Poolable;
        public float Speed;
        public float MaximumLifetime;

        private Transform _transform;
        private float _lifetime;
        private Pool _HitEffectPool;


        private void Awake()
        {
            _transform = transform;
            _HitEffectPool = PoolsCarrier.Instance.GetPoolForPrefab(HitEffectPrefab);
        }


        private void OnEnable() =>
            _lifetime = 0f;


        private void FixedUpdate()
        {
            _transform.position += Speed * Time.deltaTime * _transform.forward;
            _lifetime += Time.deltaTime;

            if (_lifetime > MaximumLifetime)
                Poolable.Return();
        }


        void OnCollisionEnter(Collision collision)
        {
            var effect = _HitEffectPool.Rent().GameObject;
            var contact = collision.GetContact(0);

            effect.transform.SetPositionAndRotation(contact.point, Quaternion.FromToRotation(Vector3.forward, contact.normal));
            effect.SetActive(true);

            Poolable.Return();
        }


        private void Reset()
        {
            Speed = 500f;
            MaximumLifetime = 5f;

            Poolable = GetComponent<Poolable>();
        }
    }
}
