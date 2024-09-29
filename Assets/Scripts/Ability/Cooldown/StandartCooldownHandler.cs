using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Ability.Cooldown
{
    [Serializable]
    public class StandartCooldownHandler : BaseCooldownControllerHandler
    {
        private const int _delay = 100;
        protected override void OnRun(float perSec)
        {
            RunAsynch(perSec);
        }

        async void RunAsynch(float perSecPoint)
        {
            while (IsRun)
            {
                await Task.Delay(_delay);
                
                AddPoint(perSecPoint);
            }
        }
    }
}