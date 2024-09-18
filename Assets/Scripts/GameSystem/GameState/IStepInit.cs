using System;

namespace GameSystem.GameState
{
    public interface IStepInit
    {
        void Execute(Action next);
    }
}