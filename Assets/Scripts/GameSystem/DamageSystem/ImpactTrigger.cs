using PropertySystem;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem.DamageSystem
{
    public class ImpactTrigger : MonoBehaviour
    {
        [SerializeField]
        private ImpactSetting ImpactSetting;

        [SerializeField] 
        private UnityEvent _onTrigger;
        
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
                    var attacker = ImpactSetting.ImpactInfo.attacker != null ? ImpactSetting.ImpactInfo.attacker : transform.root.GetComponent<PropertyCharacter>();
                    var newImpact = new ImpactInfo(reciver as PropertyCharacter,attacker,ImpactSetting.ImpactInfo.name,ImpactSetting.ImpactInfo.value);
                    reciver.Apply(newImpact);
                    
                    _onTrigger?.Invoke();
                }
            }
        }

    }
}