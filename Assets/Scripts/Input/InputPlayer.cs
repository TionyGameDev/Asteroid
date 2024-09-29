using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputPlayer : MonoBehaviour
    {
        [SerializeField] 
        private PlayerInput _playerInput;
        private InputAction _input;
        
        [Header("Names Input")]
        
        [SerializeField,ValueDropdown("GetActionNames")]
        private string _name;

        private bool _isPress;
        private Vector2 _isVector2;

        private void Awake()
        {
            if (_playerInput == null)
            {
                Debug.LogError("PlayerInput is not assigned.");
                return;
            }

            if (string.IsNullOrEmpty(_name) || _playerInput.actions[_name] == null)
            {
                Debug.LogError($"Action with name {_name} not found in PlayerInput.");
                return;
            }

            _input = _playerInput.actions[_name];
            _input.started += InputOnStarted;
            _input.performed += InputOnPerformed;
            _input.canceled += InputOnCanceled;
        }

        private void InputOnStarted(InputAction.CallbackContext obj)
        {
            
        }

        private void InputOnPerformed(InputAction.CallbackContext obj)
        {
            HandleInput(obj);
        }

        private void InputOnCanceled(InputAction.CallbackContext obj)
        {
            HandleInput(obj);
        }

        public event Action<Vector2> OnVector2Changed;
        public event Action<bool> OnPressChanged;

        private void HandleInput(InputAction.CallbackContext obj)
        {
            if (obj.valueType == typeof(Vector2))
            {
                _isVector2 = obj.ReadValue<Vector2>();
                OnVector2Changed?.Invoke(_isVector2);
            }
            else
            {
                _isPress = obj.ReadValueAsButton();
                OnPressChanged?.Invoke(_isPress);
            }
        }

        public Vector2 ReadVector2()
        {
            return _isVector2;
        }
        public bool ReadPress()
        {
            return _isPress;
        }
        
        private IEnumerable<string> GetActionNames()
        {
            if (_playerInput != null)
            {
                foreach (var action in _playerInput.actions)
                    yield return action.name;
                
            }
        }
    }
}