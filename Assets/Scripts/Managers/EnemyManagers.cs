using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events.MessageSystem;
using Events.MessageSystem.Messages;
using GameSystem;
using Player;
using Singleton;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public enum TypeEnemy
    {
        Meteor,
        UFO
    }

    [Serializable]
    public class Enemy
    {
        [SerializeField]
        private TypeEnemy _typeEnemy;
        public TypeEnemy typeEnemy => _typeEnemy;
        [SerializeField] 
        private float _speed;
        public float speed => _speed;

    }
    public class EnemyManagers : Singleton<EnemyManagers> , 
        IMessageListener<SendCreatePlayer_Msg>,
        IMessageListener<SendDeadCharacter_Msg>
    {
        [SerializeField]
        private GameObject _player;
        
        [SerializeField] 
        private List<Enemy> _enemies;
        [SerializeField] 
        private float _spawnDistance = 2f;
        [SerializeField] 
        private float randomDirectionOffset = 15f;

        [SerializeField] 
        private float _delayStart = 3f;
        [SerializeField] 
        private float _delayCreateEnemy = 0.5f;
        [SerializeField] 
        private int _countMaxEnemy;

        [SerializeField] 
        private EnemyPool _enemyPool;
        
        private Vector3 _screenBottomLeft;
        private Vector3 _screenTopRight;
        private static string path = "Prefabs/{0}";
        
        private int _index;
        private IMessageListener<SendCreatePlayer_Msg> _messageListenerImplementation;
        private IMessageListener<SendCreatePlayer_Msg> _messageListenerImplementation1;

        public void OnDestroy()
        {
            MessageBroker.localBus.broadcastChannel.Unsubscribe(this);
        }
        public void Init()
        {
            MessageBroker.localBus.broadcastChannel.Subscribe(this);
            
            Camera mainCamera = Camera.main;
            _screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            _screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            StartCoroutineEnemy(true);
        }

        private void StartCoroutineEnemy(bool start)
        {
           if (start)
                StartCoroutine(nameof(StartCreateEnemy));
           else
               StopCoroutine(nameof(StartCreateEnemy));
        }

        public void EndState()
        {
            StartCoroutineEnemy(false);
            _enemyPool.DisableAll();
        }

        private IEnumerator StartCreateEnemy()
        {
            Debug.Log("StartCreateEnemy");
            
            yield return new WaitForSeconds(_delayStart);

            while (_enemyPool.poolList.Count < _countMaxEnemy)
            {
                CreateEnemy();
                yield return new WaitForSeconds(_delayCreateEnemy);
            }
        }

        [Button]
        private void CreateEnemy()
        {
            var randomEnemy = _enemies[Random.Range(0, _enemies.Count)];
            var type = randomEnemy.typeEnemy;
            
            var obj = ResourcesLoader.LoadPrefab(string.Format(path, type));
            if (obj)
                SpawnObject(Create(obj, randomEnemy),randomEnemy);
        }
        [Button]
        public GameObject CreateEnemy(TypeEnemy typeEnemy,Vector2 position)
        {
            var obj = ResourcesLoader.LoadPrefab(string.Format(path, typeEnemy));
            var type = _enemies.FirstOrDefault(d => d.typeEnemy == typeEnemy);
            if (obj)
            {
                var enemyObj = Create(obj, type, position);
                SpawnObject(enemyObj, type);
                return enemyObj;
            }

            return null;
        }
        
        private void SpawnObject(GameObject obj, Enemy enemy)
        {
            if (obj == null) return;

            var moveEnemy = obj.GetComponent<ISetMoveEnemy>();
            if (enemy.typeEnemy == TypeEnemy.Meteor)
            {
                SetDirectionMeteor(obj, obj.transform.position, enemy.speed);
            }
            else if (enemy.typeEnemy == TypeEnemy.UFO)
            {
                moveEnemy.SetTarget(_player.transform, enemy.speed);
            }
        }
        private GameObject Create(GameObject objEnemy, Enemy enemy)
        {
            _index++;
           
             var obj = Instantiate(objEnemy, GetRandomOffScreenPosition(), Quaternion.identity);

             _enemyPool.AddToPool(obj.GetComponent<BaseEnemy>());
            
            obj.name = string.Format($"Enemy_{enemy.typeEnemy}_{_index}");
            return obj;
        }
        private GameObject Create(GameObject objEnemy, Enemy enemy,Vector2 position)
        {
            _index++;
           
            var obj = Instantiate(objEnemy, position, Quaternion.identity);

            _enemyPool.AddToPool(obj.GetComponent<BaseEnemy>());
            
            obj.name = string.Format($"Enemy_{enemy.typeEnemy}_{_index}");
            return obj;
        }

        private void SetDirectionMeteor(GameObject obj,Vector2 spawnPosition,float speed)
        {
            Vector2 directionToCenter = -spawnPosition.normalized;

            float angleOffset = Random.Range(-randomDirectionOffset, randomDirectionOffset) * Mathf.Deg2Rad;
            Vector2 finalDirection = CalculateRandomDirection(directionToCenter, angleOffset);

            obj.GetComponent<ISetMoveEnemy>().SetDirection(finalDirection, speed);
        }
        private Vector2 CalculateRandomDirection(Vector2 directionToCenter, float offset)
        {
            float angleOffset = Random.Range(-offset, offset) * Mathf.Deg2Rad;
            return new Vector2(
                directionToCenter.x * Mathf.Cos(angleOffset) - directionToCenter.y * Mathf.Sin(angleOffset),
                directionToCenter.x * Mathf.Sin(angleOffset) + directionToCenter.y * Mathf.Cos(angleOffset)
            );
        }

        private void SetTargetUFO(GameObject obj,float speed)
        {
            obj.GetComponent<ISetMoveEnemy>().SetTarget(_player.transform, speed);
        }

        Vector3 GetRandomOffScreenPosition()
        {
            float randomX, randomY;
            bool spawnVertical = Random.value > 0.5f;

            if (spawnVertical)
            {
                randomX = Random.Range(_screenBottomLeft.x, _screenTopRight.x);
                randomY = Random.value > 0.5f ? _screenTopRight.y + _spawnDistance : _screenBottomLeft.y - _spawnDistance;
            }
            else
            {
                randomX = Random.value > 0.5f ? _screenTopRight.x + _spawnDistance : _screenBottomLeft.x - _spawnDistance;
                randomY = Random.Range(_screenBottomLeft.y, _screenTopRight.y);
            }

            return new Vector3(randomX, randomY, 0);
        }


        void IMessageListener<SendDeadCharacter_Msg>.OnMessage(SendDeadCharacter_Msg msg)
        {
            if (msg.targetDead != null && !IsPlayer(msg.targetDead))
            {
                msg.targetDead.GetComponent<Destroyer>().DestroySelf();
            }
        }

        private bool IsPlayer(Transform target)
        {
            return target.GetComponent<PlayerController>() != null;
        }

        void IMessageListener<SendCreatePlayer_Msg>.OnMessage(SendCreatePlayer_Msg msg)
        {
            _player = msg.controller.gameObject;
        }
    }
}