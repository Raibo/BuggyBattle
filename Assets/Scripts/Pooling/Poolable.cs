using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.BuggyBattle.Assets.Scripts.Units.Combat;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Pooling
{
    public class Poolable : MonoBehaviour
    {
        public Pool Pool;


        public void OnParticleSystemStopped() =>
            Return();


        [ContextMenu("Return")]
        public void Return() =>
            Pool.Return(gameObject, this);


        [ResponseLocal(UnitCombetEvents.Died)]
        public void HandleUnitDeath() =>
            Return();
    }
}
