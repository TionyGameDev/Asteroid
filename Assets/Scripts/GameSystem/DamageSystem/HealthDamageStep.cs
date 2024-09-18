using System;
using PropertySystem;
using TagSystem;
using UnityEngine;
using PropertyName = PropertySystem.PropertyName;

namespace GameSystem.DamageSystem
{
    public class HealthDamageStep : IDamageStep
    {
        private Tag _deathTag = Tag.Die;
        public void Execute(ImpactInfo data, PropertyCharacter entity, Action next)
        {
            var health= entity.GetProperty(PropertyName.Health);
            if (health != null && health.currentValue > 0)
            {
                if (data.value > 0)
                {
                    health.currentValue -= data.value;
                    Debug.Log($"{entity.name} получил {data.value} урона по здоровью. Текущее здоровье: {health.currentValue}");

                    if (health.currentValue <= 0)
                        entity.AddTag(_deathTag);
                }
            }
            next();
        }
    }

}