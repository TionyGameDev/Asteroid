using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputPlayer : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset _inputAction;
        [SerializeField]
        private InputAction _input;
        public InputAction input => _input;
        
        private InputActionMap _thisInputActionMap;
        public InputActionMap thisInputActionMap => _thisInputActionMap;
        
      
        [Header("Names Input")]
        [SerializeField]
        private string actionMaps;
        [SerializeField]
        private string _name;

        private void Awake()
        {
            for (int i = 0; i < _inputAction.actionMaps.Count; i++)
            {
                if (_inputAction.actionMaps[i].name == actionMaps)
                    _thisInputActionMap = _inputAction.actionMaps[i];
            }

            _input = _thisInputActionMap[_name];
        }

        public Vector2 ReadVector2()
        {
            return _input.ReadValue<Vector2>();
        }
        public bool ReadPress()
        {
            return _input.IsPressed();
        }
    }
}