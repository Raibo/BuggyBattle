using UnityEngine;

namespace Hudossay.BuggyBattle.Assets.Scripts.Units.Combat
{
    public class Energy : MonoBehaviour, IEnergyProvider
    {
        [SerializeField] private float _maximumEnergy;
        [SerializeField] private float _currentEnergy;
        [SerializeField] private float _recoveryRate;

        public float MaximumEnergy => _maximumEnergy;
        public float CurrentEnergy => _currentEnergy;


        private void FixedUpdate()
        {
            _currentEnergy += _recoveryRate * Time.deltaTime;
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maximumEnergy);
        }


        public float TakeEnergy(float desiredAmount)
        {
            var actualAmount = Mathf.Max(desiredAmount, _currentEnergy);
            _currentEnergy -= actualAmount;
            return actualAmount;
        }
    }
}
