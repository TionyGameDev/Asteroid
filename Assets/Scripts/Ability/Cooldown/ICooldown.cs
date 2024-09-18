using System;

namespace Ability
{
    public class UpdatePointsEventArgs
    {
        public readonly float Add;
        public readonly float Curr;
        public readonly float Total;

        public UpdatePointsEventArgs(float add, float curr, float total)
        { 
            Add = add;
            Curr = curr;
            Total = total;
        }
    }
    public interface ICooldown : IDisposable
    {
        event Action OnRun;
        event Action OnComplete;
        event Action<float> OnUpdate;

        void Run();
        void Run(float total);
        void Complete();

        void SetPoints(float points);

        void SetHandler(ICooldownHandler handler);
    }
    
    public abstract class ICooldownHandler : IDisposable
    {
        public abstract event Action<float> OnAddPoint;

        public abstract void Init(CooldownController controller);
        public abstract  void Run();
        public abstract void Complete();
        public virtual void Dispose()
        {
            
        }
    }
}