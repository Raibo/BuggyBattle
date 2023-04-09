using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.AttributeEvents.Assets.Runtime.GameEvents;
using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units.Combat
{
    public class Hitpoints : MonoBehaviour, IDamageable
    {
        [EventLocal(UnitCombetEvents.HitpointsChanged)] public GameEvent<float> HitpointsChanged;
        [EventLocal(UnitCombetEvents.Died)] public GameEvent UnitDied;

        [SerializeField] private float _maximumHitpoints;
        [SerializeField] private float _currentHitpoints;

        public float MaximumHitpoints => _maximumHitpoints;

        public float CurrentHitpoints
        {
            get => _currentHitpoints;

            set
            {
                _currentHitpoints = value;
                HitpointsChanged.Raise(_currentHitpoints);

                if (_currentHitpoints <= 0f)
                    UnitDied.Raise();
            }
        }


        public void InflictDamage(float amount) =>
            CurrentHitpoints -= amount;
    }
}
