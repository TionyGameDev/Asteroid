using System;
using PropertySystem;
using UnityEngine;
using PropertyName = PropertySystem.PropertyName;

namespace GameSystem.DamageSystem
{
    public class ShieldDamageStep : IDamageStep
    {
        public void Execute(ImpactInfo data, PropertyCharacter entity, Action next)
        {
            var shield= entity.GetProperty(PropertyName.Shield);
            if (shield != null && shield.currentValue > 0)
            {
                float shieldDamage = Mathf.Min(shield.currentValue, data.value);
                shield.currentValue -= shieldDamage;
                data.value -= shieldDamage;
                    
                Debug.Log($"{entity.name} поглотил {shieldDamage} урона щитом. Осталось урона: {data.value}");
            }

            next();
        }

    }
}