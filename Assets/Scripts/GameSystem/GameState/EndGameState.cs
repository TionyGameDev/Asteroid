using UnityEngine;

namespace GameSystem.GameState
{
    public class EndGameState : GameState
    {
        public override void Enter()
        {
            Time.timeScale = 0;
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}