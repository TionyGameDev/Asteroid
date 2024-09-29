using System;
using Input;
using Player;
using UnityEngine;

namespace Ability
{
    public class MoveAbility : Ability
    {
        private bool _isAlive = true;

        [SerializeField] 
        private float _shipAcceleration;
        [SerializeField]
        private float _shipMaxVelocity;
        [SerializeField] 
        private float _shipRotationSpeed;

        private void Start()
        {
            _inputPlayer.OnVector2Changed += InputPlayerOnOnVector2Changed;
        }

        private void InputPlayerOnOnVector2Changed(Vector2 obj)
        {
            _input = obj;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (_isAlive) 
            {
                _rigidbody.AddForce(_rigidbody.transform.up * (_shipAcceleration * _input.y));
                _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _shipMaxVelocity);
                
                _rigidbody.transform.Rotate(_rigidbody.transform.forward * (_shipRotationSpeed * Time.deltaTime * -_input.x));
            }
        }
    }
}