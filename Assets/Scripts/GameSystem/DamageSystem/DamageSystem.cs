using System.Collections.Generic;
using PropertySystem;
using UnityEngine;

namespace GameSystem.DamageSystem
{
    [CreateAssetMenu(fileName = "DamageSystem", menuName = "System", order = 1)]
    public class DamageSystem : ScriptableObject
    {
        private List<IDamageStep> damageSteps;

        public DamageSystem()
        {
            damageSteps = new List<IDamageStep>
            {
                new ShieldDamageStep(),
                new HealthDamageStep()
            };
        }

        public void Apply(ImpactInfo data)
        {
            int currentStep = 0;

            System.Action nextStep = null;
            nextStep = () =>
            {
                if (currentStep < damageSteps.Count)
                {
                    IDamageStep step = damageSteps[currentStep];
                    currentStep++;
                    step.Execute(data, data.target, nextStep);
                }
            };

            nextStep();
        }
            
        
    }
}