using System;
using PropertySystem;
using UnityEngine;

namespace Bullets
{
    public abstract class BulletEntity : MonoBehaviour , IBullet
    {
        public ImpactSetting ImpactSetting;
        public void Init(ImpactSetting impact)
        {
            ImpactSetting = impact;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((ImpactSetting.layerMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                var reciver = other.GetComponent<IImpactable>();
                if (reciver != null)
                {
                    var newImpact = new ImpactInfo(reciver as PropertyCharacter,ImpactSetting.ImpactInfo.attacker,ImpactSetting.ImpactInfo.name,ImpactSetting.ImpactInfo.value);
                    reciver.Apply(newImpact);
                }
            }
            else
            {
                Debug.Log("Not in Layermask");
            }
        }
        
        void IBullet.Dispose()
        {
            
        }
    }
}