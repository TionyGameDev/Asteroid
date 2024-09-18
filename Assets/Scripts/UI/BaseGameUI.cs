using System;
using Assets.Scripts.MVC;
using Events.MessageSystem;
using Events.MessageSystem.Messages;
using GameSystem.GameState;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BaseGameUI : MonoBehaviour, IGameUI
        ,IMessageListener<SendNewPosition_Msg>
        ,IMessageListener<SendCreatePlayer_Msg>
        ,IMessageListener<SendDeadCharacter_Msg>
    {
        [SerializeField] 
        private TextMeshProUGUI _coordinateText;
        [SerializeField] 
        private TextMeshProUGUI _rotationText;
        [SerializeField] 
        private TextMeshProUGUI _speedText;

        [SerializeField] private GameObject _defeat;
        
        private PlayerController _controller;
        private Rigidbody2D _rigidbodyPlayer;
        private int _lastRotation = 0;
        private int _lastSpeed;
        
        private void Awake()
        {
            ((IGameUI) this).Init();
        }

        public void Init()
        {
            MessageBroker.localBus.broadcastChannel.Subscribe(this);
        }

        public void Dispose()
        {
            MessageBroker.localBus.broadcastChannel.Unsubscribe(this);
        }

        private void Update()
        {
            if (_controller == null)
                return;
            
            var rotationZ = (int) _controller.transform.eulerAngles.z;
            var speed = (int) _rigidbodyPlayer.velocity.magnitude;
            
            if (_lastRotation != rotationZ)
                DrawRotation(rotationZ);
            
            if (_lastSpeed != speed)
                DrawSpeed(speed);
        }

        private void DrawRotation(int rotation)
        {
            _lastRotation = rotation;
            
            if (_rotationText)
                _rotationText.text = rotation + "%";
        }

        private void DrawSpeed(int speed)
        {
            _lastSpeed = speed;
            if (_speedText)
                _speedText.text = String.Format("Speed: {0}",speed);
        }

        public void OnMessage(SendNewPosition_Msg message)
        {
            if (_coordinateText)
                _coordinateText.text = string.Format("{0},{1}", message.position.x, message.position.y);
        }

        public void OnMessage(SendDeadCharacter_Msg message)
        {
            if (message.targetDead.GetComponent<PlayerController>())
            {
                if (_defeat)
                {
                    _defeat.gameObject.SetActive(true);
                    GameStateMachine.Instance.SetState(new EndGameState());
                }
            }
        }

        public void Restart()
        {
            GameStateMachine.Instance.SetState(new StartGameState());
        }

        public void OnMessage(SendCreatePlayer_Msg message)
        {
            _controller = message.controller;
            _rigidbodyPlayer = _controller.GetComponent<Rigidbody2D>();
        }
    }
}