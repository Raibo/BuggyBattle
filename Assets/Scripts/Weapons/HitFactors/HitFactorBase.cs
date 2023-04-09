using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Weapons
{
    public abstract class HitFactorBase : ScriptableObject
    {
        public abstract void PerformHit(Collision collision);
    }
}
