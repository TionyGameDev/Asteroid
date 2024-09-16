using Singleton;
using UnityEngine;

namespace Managers
{
    public class ScreenWrapManager : Singleton<ScreenWrapManager>
    {
        [SerializeField]
        private GameObject _player;

        private float leftBoundary;
        private float rightBoundary;
        private float topBoundary;
        private float bottomBoundary;

        protected override void Awake()
        {
            base.Awake();
            InitializeBoundaries();
        }

        public void SetPlayer(GameObject player)
        {
            _player = player;
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

        private void ActivateClone(Vector3 currentPosition, float width, float height)
        {
            Vector3 offset = Vector3.zero;

            if (currentPosition.x > rightBoundary - 0.5f) offset.x = -width;
            else if (currentPosition.x < leftBoundary + 0.5f) offset.x = width;

            if (currentPosition.y > topBoundary - 0.5f) offset.y = -height;
            else if (currentPosition.y < bottomBoundary + 0.5f) offset.y = height;
        }
    }
}
