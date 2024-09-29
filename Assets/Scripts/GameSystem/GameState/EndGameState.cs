using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameState
{
    public class EndGameState : GameState
    {
        public override void Enter()
        {
            EnemyManagers.Instance.EndState();
            Time.timeScale = 0;
        }

        public override void Exit()
        {
            SceneManager.LoadScene("Game");
        }

        public override void Update()
        {
            
        }
    }
}