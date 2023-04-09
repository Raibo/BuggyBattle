using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Weapons.HitFactors
{
    [CreateAssetMenu(fileName = nameof(ExplosionPush), menuName = "Scriptable Objects/HitFactors/" + nameof(ExplosionPush))]
    public class ExplosionPush : HitFactorBase
    {
        public float Force;
        public float Radius;
        public LayerMask LayerMask;


        public override void PerformHit(Collision collision)
        {
            var explosionCenter = collision.contacts[0].point;
            var targetColliders = Physics.OverlapSphere(explosionCenter, Radius, LayerMask);

            foreach (var targetCollider in targetColliders)
            {
                if (!targetCollider.gameObject.TryGetComponent<Rigidbody>(out var pushedBody))
                    continue;

                pushedBody.AddExplosionForce(Force, explosionCenter, Radius);
            }
        }
    }
}
