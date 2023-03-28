using Hudossay.BuggyBattle.Assets.Scripts.Pooling;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        public Poolable Poolable;
        public float Speed;
        public float MaximumLifetime;

        private Transform _transform;
        private float _lifetime;

        private void Awake() =>
            _transform = transform;


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
            print("Hit");
        }


        void OnTriggerEnter(Collider other)
        {
            print("Hit");
        }


        private void Reset()
        {
            Speed = 500f;
            MaximumLifetime = 5f;

            Poolable = GetComponent<Poolable>();
        }
    }
}
