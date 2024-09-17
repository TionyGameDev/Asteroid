using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Ability.Cooldown
{
    [Serializable]
    public class ByTimeCooldownHandler : BaseCooldownControllerHandler
    {
        private const int _points = 60;
        private const int _perSec = 1;


        protected override void OnRun()
        {
            base.OnRun();

            RunAsynch();
        }


        async void RunAsynch()
        {
            while (IsRun)
            {
                await Task.Delay((int)(_perSec * 1000));
                
                AddPoint(_points);
            }
        }
    }
}