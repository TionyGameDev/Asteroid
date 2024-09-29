using Events.MessageSystem;
using Events.MessageSystem.Messages;
using Singleton;
using UnityEngine;

namespace Managers
{
    public class ScreenManager : Singleton<ScreenManager>,
        IMessageListener<SendCreatePlayer_Msg>
    {
        [SerializeField]
        private GameObject _player;

        private Camera _mainCamera;
        private float leftBoundary;
        private float rightBoundary;
        private float topBoundary;
        private float bottomBoundary;
        private Vector2 _lastPosition = Vector2.zero;

        public void Init()
        {
            InitializeBoundaries();
            MessageBroker.localBus.broadcastChannel.Subscribe(this);
        }

        private void OnEnable()
        {
            MessageBroker.localBus.broadcastChannel.Subscribe(this);
        }

        private void OnDisable()
        {
            MessageBroker.localBus.broadcastChannel.Unsubscribe(this);
        }

        private void InitializeBoundaries()
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null)
            {
                Debug.LogError("Main Camera not found!");
                return;
            }

            leftBoundary = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            rightBoundary = _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            topBoundary = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            bottomBoundary = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
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
            var x = Mathf.Clamp(pos.x, leftBoundary, rightBoundary);
            var y = Mathf.Clamp(pos.y, bottomBoundary, topBoundary);
            var newPosition = new Vector2(x, y);

            if (_lastPosition != newPosition)
            {
                _lastPosition = newPosition;
                MessageBroker.localBus.broadcastChannel.SendMessage(new SendNewPosition_Msg(_lastPosition));
            }
        }

        public void UpdateObjectPosition()
        {
            Vector3 position = _player.transform.position;

            position.x = WrapPosition(position.x, leftBoundary, rightBoundary, rightBoundary - leftBoundary);
            position.y = WrapPosition(position.y, bottomBoundary, topBoundary, topBoundary - bottomBoundary);

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
