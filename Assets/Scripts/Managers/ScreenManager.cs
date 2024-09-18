using System;
using Events.MessageSystem;
using Events.MessageSystem.Messages;
using Singleton;
using UnityEngine;

namespace Managers
{
    public class ScreenManager : Singleton<ScreenManager> ,
        IMessageListener<SendCreatePlayer_Msg>
    {
        [SerializeField]
        private GameObject _player;
         
        private float leftBoundary;
        private float rightBoundary;
        private float topBoundary;
        private float bottomBoundary;
        private Vector2 _lastPosition = Vector2.zero;
        public void Init()
        {
            MessageBroker.localBus.broadcastChannel.Subscribe(this);
            InitializeBoundaries();
        }
        private void OnDestroy()
        {
            MessageBroker.localBus.broadcastChannel.Unsubscribe(this);
        }

        private void InitializeBoundaries()
        {
            if (Camera.main is not null)
            {
                leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
                rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
                topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
                bottomBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            }
        }

        private void Update()
        {
            if (_player == null)
                return;
            
            UpdateObjectPosition();
            UpdateCoordinate();
        }

        private void UpdateCoordinate()
        {
            var pos = _player.transform.position;
            var x = (int) Mathf.Clamp(pos.x, leftBoundary, rightBoundary);
            var y = (int) Mathf.Clamp(pos.y, bottomBoundary, topBoundary);
            var newPosition = new Vector2(x,y);
            
            if (_lastPosition != newPosition)
            {
                _lastPosition = newPosition;
                 MessageBroker.localBus.broadcastChannel.SendMessage(new SendNewPosition_Msg(_lastPosition));
            }
           
        }

        public void UpdateObjectPosition()
        {
            Vector3 position = _player.transform.position;

            float width = rightBoundary - leftBoundary;
            float height = topBoundary - bottomBoundary;

            position.x = WrapPosition(position.x, leftBoundary, rightBoundary, width);
            position.y = WrapPosition(position.y, bottomBoundary, topBoundary, height);

            _player.transform.position = position;
        }

        private float WrapPosition(float pos, float min, float max, float range)
        {
            if (pos > max) return pos - range;
            if (pos < min) return pos + range;
            return pos;
        }

        public void OnMessage(SendCreatePlayer_Msg message)
        {
            _player = message.controller.gameObject;
        }
    }
}