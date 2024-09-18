using System;
using System.Threading.Tasks;
using Managers;

namespace GameSystem.GameState.Step
{
    public class InitializationManagers : IStepInit
    {
        public async void Execute(Action next)
        {
            ScreenManager.Instance.Init();
            EnemyManagers.Instance.Init();
            await Task.Delay(100);
            next();
        }
    }
}