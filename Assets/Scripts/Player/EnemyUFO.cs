using System;
using PropertySystem;
using UnityEngine;

namespace Player
{
    public class EnemyUFO : CharacterEntity , ISetMoveEnemy
    {
        private Transform _target;
        private float _speed;
        public void SetDirection(Vector3 direction, float speed)
        {
            
        }

        public void SetTarget(Transform target, float speed)
        {
            _target = target;
            _speed = speed;
        }

        private void FixedUpdate()
        {
            if (_target == null)
                return;
            
            Vector2 direction = (_target.position - transform.position).normalized;
            
            _rigidbody.velocity = direction * _speed;
        }
    }
}