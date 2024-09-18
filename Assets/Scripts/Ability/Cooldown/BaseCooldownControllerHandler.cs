using System;
using UnityEngine;

namespace Ability
{
    [Serializable]
    public abstract class BaseCooldownControllerHandler : ICooldownHandler
    {
        public override event Action<float> OnAddPoint;

        protected bool IsRun { get; private set; }

        public override void Run()
        {
            if (IsRun) return;

            IsRun = true;

            OnRun();
        }

        protected virtual void OnRun() { }

        public override void Complete()
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
        public override void Init(CooldownController controller)
        {
            this.controller = controller;

            OnInit(controller);
        }

        protected virtual void OnInit(CooldownController controller) { }
        public override void Dispose()
        {
            IsRun = false;
        }
    }
}