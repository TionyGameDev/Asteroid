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

    public interface ICooldownPointsProvider
    {
        float GetCooldownPoints();
    }
    
    public interface ICooldownHandler : IDisposable
    {
        event Action<float> OnAddPoint;

        void Init(CooldownController controller);
        void Run();
        void Complete();
    }
}