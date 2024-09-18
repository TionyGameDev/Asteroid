using System;
using Singleton;
using UnityEngine;

namespace GameSystem.GameState
{
    public class GameStateMachine : Singleton<GameStateMachine>
    {
        private GameState _currentState;
        private GameState _startState;
        private GameState _gameState;
        private GameState _endState;

        private void Start()
        {
            _startState = new StartGameState();

            SetState(_startState);
        }

        public void SetState(GameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}