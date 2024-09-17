using System;
using UnityEngine;

namespace Ability
{
    [Serializable]
    public abstract class BaseCooldownControllerHandler : ICooldownHandler
    {
        
        public event Action<float> OnAddPoint;

        protected bool IsRun { get; private set; }

        public void Run()
        {
            if (IsRun) return;

            IsRun = true;

            OnRun();
        }

        protected virtual void OnRun() { }

        public void Complete()
        {
            if (!IsRun) return;

            IsRun = false;
            
            OnComplete();
        }

        protected virtual void OnComplete() { }


        protected void AddPoint(float points)
        {
            OnAddPoint?.Invoke(points);
        }

        protected CooldownController controller { get; private set; }
        public void Init(CooldownController controller)
        {
            this.controller = controller;

            OnInit(controller);
        }

        protected virtual void OnInit(CooldownController controller) { }
        public void Dispose()
        {
            IsRun = false;
        }
    }
}