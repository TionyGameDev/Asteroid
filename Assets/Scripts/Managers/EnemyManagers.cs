using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events.MessageSystem;
using Events.MessageSystem.Messages;
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
        private List<GameObject> _enemiesObjects;
        private Vector3 _screenBottomLeft;
        private Vector3 _screenTopRight;
        private static string path = "Prefabs/{0}";
        
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

            StartCoroutine(nameof(StartCreateEnemy));
        }

        private IEnumerator StartCreateEnemy()
        {
            Debug.Log("StartCreateEnemy");
            
            yield return new WaitForSeconds(_delayStart);

            while (_enemiesObjects.Count < _countMaxEnemy)
            {
                CreateEnemy();
                yield return new WaitForSeconds(_delayCreateEnemy);
            }
        }

        [Button]
        public void CreateEnemy()
        {
            var randomEnemy = _enemies[Random.Range(0, _enemies.Count)];
            var type = randomEnemy.typeEnemy;
            
            var pathEnemy = string.Format(path, type);
            Debug.Log(pathEnemy);
            var obj = ResourcesLoader.LoadPrefab(string.Format(path, type));
            if (obj)
                SpawnObject(obj,randomEnemy);
        }
        [Button]
        public void CreateEnemy(TypeEnemy typeEnemy)
        {
            var pathEnemy = string.Format(path, typeEnemy);
            var obj = ResourcesLoader.LoadPrefab(string.Format(path, pathEnemy));
            if (obj)
                SpawnObject(obj,_enemies.FirstOrDefault(d => d.typeEnemy == typeEnemy));
        }
        
        void SpawnObject(GameObject objEnemy,Enemy enemy)
        {
            _enemiesObjects.Add(objEnemy);
            var obj = Instantiate(objEnemy, GetRandomOffScreenPosition(), Quaternion.identity);
           if (obj && enemy.typeEnemy == TypeEnemy.Meteor)
               SetDirectionMeteor(obj, obj.transform.position,enemy.speed);
           else if (obj && enemy.typeEnemy == TypeEnemy.UFO)
               SetTargetUFO(obj,enemy.speed);
               
        }

        private void SetDirectionMeteor(GameObject obj,Vector2 spawnPosition,float speed)
        {
            Vector2 directionToCenter = -spawnPosition.normalized;

            float angleOffset = Random.Range(-randomDirectionOffset, randomDirectionOffset) * Mathf.Deg2Rad;
            Vector2 finalDirection = new Vector2(
                directionToCenter.x * Mathf.Cos(angleOffset) - directionToCenter.y * Mathf.Sin(angleOffset),
                directionToCenter.x * Mathf.Sin(angleOffset) + directionToCenter.y * Mathf.Cos(angleOffset)
            );

            obj.GetComponent<ISetMoveEnemy>().SetDirection(finalDirection, speed);
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
                randomY = (Random.value > 0.5f) ? _screenTopRight.y + Random.Range(0, _spawnDistance) : _screenBottomLeft.y - Random.Range(0, _spawnDistance);
            }
            else
            {
                randomX = (Random.value > 0.5f) ? _screenTopRight.x + Random.Range(0, _spawnDistance) : _screenBottomLeft.x - Random.Range(0, _spawnDistance);
                randomY = Random.Range(_screenBottomLeft.y, _screenTopRight.y);
            }

            return new Vector3(randomX, randomY, 0);
        }

        public void OnMessage(SendCreatePlayer_Msg message)
        {
            Debug.Log(message.controller);
            _player = message.controller.gameObject;
        }

        public void OnMessage(SendDeadCharacter_Msg message)
        {
            _enemiesObjects.Remove(message.targetDead.gameObject);
        }
    }
}