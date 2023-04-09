namespace Hudossay.BuggyBattle.Assets.Scripts.Units.Combat
{
    public interface IEnergyProvider
    {
        float MaximumEnergy { get; }
        float CurrentEnergy { get; }
        float TakeEnergy(float desiredAmount);
    }
}
