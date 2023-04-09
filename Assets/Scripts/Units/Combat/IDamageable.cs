namespace Hudossay.BuggyBattle.Assets.Scripts.Units.Combat
{
    public interface IDamageable
    {
        void InflictDamage(float amount);
        float MaximumHitpoints { get; }
        float CurrentHitpoints { get; }
    }
}
