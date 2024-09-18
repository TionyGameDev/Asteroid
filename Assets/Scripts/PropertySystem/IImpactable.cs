using System;
using UnityEngine;

namespace PropertySystem
{
    public interface IImpactable
    {
        void Apply(ImpactInfo impactInfo);
    }

    [Serializable]
    public class ImpactInfo
    {
        [HideInInspector]
        public PropertyCharacter attacker;
        [HideInInspector]
        public PropertyCharacter target;
        public PropertyName name;
        public float value;

        public ImpactInfo(PropertyCharacter target)
        {
            this.target = target;
        }
        public ImpactInfo(PropertyCharacter target,PropertyCharacter attacker,PropertyName name,float value)
        {
            this.attacker = attacker;
            this.target = target;
            this.name = name;
            this.value = value;
        }
        
    }
    
    [Serializable]
    public class ImpactSetting
    {
        public LayerMask layerMask;
        public ImpactInfo ImpactInfo;
        
    }
}