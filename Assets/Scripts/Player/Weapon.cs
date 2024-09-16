using Bullets;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] 
        private Transform _point;
        
        public Rigidbody2D InstantiateBullet(Rigidbody2D bullet,Vector3 direction)
        {
            var rb = Instantiate(bullet, _point.position, Quaternion.identity);
            if (rb)
            {
                //rb.velocity = direction;
            }

            return rb;
        }
    }
}