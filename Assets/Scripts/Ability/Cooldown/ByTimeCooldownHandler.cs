using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Ability.Cooldown
{
    [Serializable]
    public class ByTimeCooldownHandler : BaseCooldownControllerHandler
    {
        [SerializeField]
        private float _points;
        [SerializeField]
        private float _perSec;


        protected override void OnRun()
        {
            base.OnRun();
            Debug.Log("OnRun");

            RunAsynch();
        }


        async void RunAsynch()
        {
            while (IsRun && _perSec > 0 && _points > 0)
            {
                await Task.Delay((int)(_perSec * 1000));

                Debug.Log("AddPoint");

                AddPoint(_points);
            }
        }
    }
}