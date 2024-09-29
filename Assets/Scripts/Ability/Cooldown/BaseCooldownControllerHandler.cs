using System;

namespace Ability.Cooldown
{
    [Serializable]
    public abstract class BaseCooldownControllerHandler : ICooldownHandler
    {
        public override event Action<float> OnAddPoint;

        protected bool IsRun { get; private set; }

        public override void Run(float perSec)
        {
            if (IsRun) return;

            IsRun = true;

            OnRun(perSec);
        }

        protected virtual void OnRun(float perSec) { }

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