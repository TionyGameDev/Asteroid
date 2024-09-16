using System;
using Input;
using Player;
using UnityEngine;

namespace Ability
{
    public class ShootAbility : Ability
    {
        [SerializeField] 
        private Rigidbody2D _bullet;

        [SerializeField] 
        private Weapon _weapon;

        [SerializeField] 
        private float bulletSpeed = 8f;
        
        private bool _press;

        private void Update()
        {
            _press = _inputPlayer.ReadPress();
            if (_press && !_blocked)
                HandleShooting();
        }

        private void HandleShooting() 
        {
            _onActive?.Invoke();
            Vector2 shipVelocity = _rigidbody.velocity;
            Vector2 shipDirection = transform.root.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);
            var direction = shipDirection * shipForwardSpeed;
            
            var bullet = _weapon.InstantiateBullet(_bullet,direction);

            bullet.velocity = shipDirection * shipForwardSpeed;

            // Add force to propel bullet in direction the player is facing.
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
           
        }

    }
}
