using Hudossay.BuggyBattle.Assets.Scripts.Units.Combat;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Weapons.HitFactors
{
    [CreateAssetMenu(fileName = nameof(ExplosionDamage), menuName = "Scriptable Objects/HitFactors/" + nameof(ExplosionDamage))]
    public class ExplosionDamage : HitFactorBase
    {
        public float Damage;
        public float Radius;
        public LayerMask LayerMask;
        public AnimationCurve DamageCurve;


        public override void PerformHit(Collision collision)
        {
            var explosionCenter = collision.contacts[0].point;
            var targetColliders = Physics.OverlapSphere(explosionCenter, Radius, LayerMask);

            foreach (var targetCollider in targetColliders)
            {
                if (!targetCollider.gameObject.TryGetComponent<IDamageable>(out var damageable))
                    continue;

                var point = targetCollider.ClosestPoint(explosionCenter);
                var distanceOnCurve = Vector3.Distance(point, explosionCenter) / Radius;
                var damage = DamageCurve.Evaluate(distanceOnCurve);

                damageable.InflictDamage(damage);
            }
        }
    }
}
