using Hudossay.BuggyBattle.Assets.Scripts.Units.Combat;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Weapons.HitFactors
{
    [CreateAssetMenu(fileName = nameof(SingleTargetDamage), menuName = "Scriptable Objects/HitFactors/" + nameof(SingleTargetDamage))]
    public class SingleTargetDamage : HitFactorBase
    {
        public float Damage;


        public override void PerformHit(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
                return;

            damageable.InflictDamage(Damage);
        }
    }
}
