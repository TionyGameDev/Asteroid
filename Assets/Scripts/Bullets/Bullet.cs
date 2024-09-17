using System;
using UnityEngine;

namespace Bullets
{
    public class Bullet : BulletEntity     
    {
        [SerializeField] 
        private Rigidbody2D _rigidbody;
        [SerializeField] 
        private float _speed;

        private void FixedUpdate()
        {
            if (_rigidbody)
                _rigidbody.velocity = transform.up * _speed;
        }
    }
}