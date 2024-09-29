using Managers;
using PropertySystem;
using UnityEngine;

namespace Player
{
    public class EnemyAsteroid : BaseEnemy , ISetMoveEnemy
    {
        private int size = 1;

        protected override void OnDie()
        {
            base.OnDie();
            
            if (size >= 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    var obj = EnemyManagers.Instance.CreateEnemy(typeEnemy,this.transform.position);
                    obj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                    var asteroid = obj.GetComponent<EnemyAsteroid>();
                    if (asteroid)
                        asteroid.SetSize(1);
                }
            }
        }

        public void SetSize(int sizes)
        {
            size -= sizes;
        }
        public void SetDirection(Vector3 direction, float speed)
        {
            _rigidbody.velocity = direction.normalized * speed;
        }

        public void SetTarget(Transform target, float speed)
        {
            
        }
    }
}