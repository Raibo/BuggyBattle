using Hudossay.AttributeEvents.Assets.Runtime.Attributes;
using Hudossay.AttributeEvents.Assets.Runtime.GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Hudossay.BuggyBattle.Assets.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        public float MouseSensitivity;

        [EventGlobal(InputEvent.Aim)] public GameEvent<Vector2> Aim;
        [EventGlobal(InputEvent.Movement)] public GameEvent<Vector2> Movement;

        [EventGlobal(InputEvent.Weapon1Started)] public GameEvent Weapon1Started;
        [EventGlobal(InputEvent.Weapon1Canceled)] public GameEvent Weapon1Canceled;

        [EventGlobal(InputEvent.Weapon2Started)] public GameEvent Weapon2Started;
        [EventGlobal(InputEvent.Weapon2Canceled)] public GameEvent Weapon2Canceled;

        [EventGlobal(InputEvent.Weapon3Started)] public GameEvent Weapon3Started;
        [EventGlobal(InputEvent.Weapon3Canceled)] public GameEvent Weapon3Canceled;

        [EventGlobal(InputEvent.Menu)] public GameEvent Menu;


        public void OnAim(CallbackContext cc)
        {
            var vector = cc.ReadValue<Vector2>();
            Aim.Raise(vector * MouseSensitivity);
        }


        public void OnMove(CallbackContext cc)
        {
            var vector = cc.ReadValue<Vector2>();
            Movement.Raise(vector);
        }


        public void OnWeapon1(CallbackContext cc)
        {
            switch (cc.phase)
            {
                case InputActionPhase.Started:
                    Weapon1Started.Raise();
                    break;
                case InputActionPhase.Canceled:
                    Weapon1Canceled.Raise();
                    break;
            }
        }


        public void OnWeapon2(CallbackContext cc)
        {
            switch (cc.phase)
            {
                case InputActionPhase.Started:
                    Weapon2Started.Raise();
                    break;
                case InputActionPhase.Canceled:
                    Weapon2Canceled.Raise();
                    break;
            }
        }


        public void OnWeapon3(CallbackContext cc)
        {
            switch (cc.phase)
            {
                case InputActionPhase.Started:
                    Weapon3Started.Raise();
                    break;
                case InputActionPhase.Canceled:
                    Weapon3Canceled.Raise();
                    break;
            }
        }


        public void OnMenu(CallbackContext cc)
        {
            if (cc.phase != InputActionPhase.Performed)
                return;

            Menu.Raise();
        }


        private void Reset()
        {
            MouseSensitivity = 1f;
        }
    }
}
