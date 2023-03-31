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
        private Rigidbody _rigidbody;
        private TrailRenderer _trail;
        private float _lifetime;
        private Pool _HitEffectPool;


        private void Awake()
        {
            _transform = transform;
            _HitEffectPool = PoolsCarrier.Instance.GetPoolForPrefab(HitEffectPrefab);
            _rigidbody = GetComponent<Rigidbody>();
            TryGetComponent(out _trail);
        }


        private void OnEnable()
        {
            _lifetime = 0f;
            _rigidbody.velocity = _transform.TransformDirection(new Vector3(0f, 0f, Speed));
            _rigidbody.angularVelocity = Vector3.zero;
            _trail?.Clear();
        }


        private void FixedUpdate()
        {
            _lifetime += Time.deltaTime;

            if (_lifetime > MaximumLifetime)
                Poolable.Return();
        }


        void OnCollisionEnter(Collision collision)
        {
            var contact = collision.GetContact(0);

            var effect = _HitEffectPool.Rent().GameObject;
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
