using System;
using GameSystem.DamageSystem;
using PropertySystem;
using UnityEngine;

namespace Bullets
{
    public abstract class BulletEntity : MonoBehaviour , IBullet
    {
        [SerializeField]
        private ImpactTrigger _impactSetting;
        public void Init(ImpactSetting impact)
        {
            _impactSetting.Init(impact);
        }
        void IBullet.Dispose()
        {
            
        }
    }
}