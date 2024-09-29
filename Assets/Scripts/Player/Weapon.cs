using Bullets;
using PropertySystem;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] 
        private Transform _point;
        public Transform point => _point;
        
        public void InstantiateBullet(ImpactSetting impact, Bullet bullet, Quaternion quaternion)
        {
            var rb = Instantiate(bullet, _point.position, quaternion);
            if (rb)
                rb.Init(impact);
        }

    }
}