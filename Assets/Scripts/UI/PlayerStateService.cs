using System;
using Player;
using UnityEngine;

namespace UI
{
    public class PlayerStateService
    {
        public event Action<int> OnRotationChanged;
        public event Action<int> OnSpeedChanged;

        private PlayerController _controller;
        private Rigidbody2D _rigidbodyPlayer;

        private int _lastRotation;
        private int _lastSpeed;

        public void Init(PlayerController controller)
        {
            _controller = controller;
            _rigidbodyPlayer = _controller.GetComponent<Rigidbody2D>();
        }

        public void UpdateState()
        {
            if (_controller == null || _rigidbodyPlayer == null)
                return;

            int currentRotation = (int) _controller.transform.eulerAngles.z;
            int currentSpeed = (int) _rigidbodyPlayer.velocity.magnitude;

            if (currentRotation != _lastRotation)
            {
                _lastRotation = currentRotation;
                OnRotationChanged?.Invoke(currentRotation);
            }

            if (currentSpeed != _lastSpeed)
            {
                _lastSpeed = currentSpeed;
                OnSpeedChanged?.Invoke(currentSpeed);
            }
        }
    }
}