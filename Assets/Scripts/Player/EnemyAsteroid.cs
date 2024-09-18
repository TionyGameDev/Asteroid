using PropertySystem;
using UnityEngine;

namespace Player
{
    public class EnemyAsteroid : CharacterEntity , ISetMoveEnemy
    {
        public void SetDirection(Vector3 direction, float speed)
        {
            _rigidbody.velocity = direction.normalized * speed;
        }

        public void SetTarget(Transform target, float speed)
        {
            
        }
    }
}