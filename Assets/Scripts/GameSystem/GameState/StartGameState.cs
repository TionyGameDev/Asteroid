using System.Collections.Generic;
using GameSystem.DamageSystem;
using GameSystem.GameState.Step;
using UnityEngine;

namespace GameSystem.GameState
{
    public class StartGameState : GameState
    {
        private List<IStepInit> _startStep;
        
        public StartGameState()
        {
            _startStep = new List<IStepInit>
            {
                new InitializationManagers(),
                new InstantiateStep()
            };
        }
        public override void Enter()
        {
            Time.timeScale = 1; 
            int currentStep = 0;

            System.Action nextStep = null;
            nextStep = () =>
            {
                if (currentStep < _startStep.Count)
                {
                    IStepInit step = _startStep[currentStep];
                    currentStep++;
                    step.Execute(nextStep);
                }
            };
            nextStep();
        }

        public override void Exit()
        {
            Debug.Log("END");
        }

        public override void Update()
        {
            
        }
    }
}