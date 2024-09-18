using UnityEngine;

namespace Player
{
    public interface ISetMoveEnemy
    {
        public void SetDirection(Vector3 direction, float speed);
        public void SetTarget(Transform target, float speed);
    }
}