using System;
using Ability.Cooldown;

namespace Ability
{
    public interface ICooldown : IDisposable
    {
        event Action OnRun;
        event Action OnComplete;
        event Action<float> OnUpdate;

        void Run(float perSec,float total);
        void Complete();

        void SetPoints(float perSec,float points);

        void SetHandler(ICooldownHandler handler);
    }
    
    public abstract class ICooldownHandler : IDisposable
    {
        public abstract event Action<float> OnAddPoint;

        public abstract void Init(CooldownController controller);
        public abstract  void Run(float perSec);
        public abstract void Complete();
        public virtual void Dispose()
        {
            
        }
    }
}