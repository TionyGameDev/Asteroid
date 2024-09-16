using System;
using Input;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ability
{
    public interface IAbility
    {
        void Init();
        void Dispose();
    }

    public abstract class Ability : MonoBehaviour, IAbility
    {
        protected Action _onActive;

        public bool _blocked;
        
        [SerializeField] 
        private CooldownController _cooldownController;

        [SerializeReference,] 
        private ICooldownHandler _cooldownHandler;
        private bool useCooldown => !(_cooldownHandler is NoneCooldownHandler);

        [SerializeField,ShowIf(nameof(useCooldown))] 
        private float _cooldownTime;
        [SerializeField] 
        protected InputPlayer _inputPlayer;
        
        protected PlayerController _player;
        protected Vector2 _input;
        protected Rigidbody2D _rigidbody;
        
        void IAbility.Init()
        {
            Debug.Log("Init");
            var root = transform.root;
            _player = root.GetComponent<PlayerController>();
            _rigidbody =  root.GetComponent<Rigidbody2D>();
            
            if (_cooldownController)
            {
                _cooldownController.SetHandler(_cooldownHandler);

                _cooldownController.OnRun += CooldownControllerOnOnRun;
                _cooldownController.OnComplete += CooldownControllerOnOnComplete;
            }
            
            _onActive += OnActiveSkill;
        }

        private void OnActiveSkill()
        {
            _blocked = true;
            Debug.Log(nameof(OnActiveSkill));
            if (_cooldownController)
                _cooldownController.Run(_cooldownTime * 60);
        }

        private void CooldownControllerOnOnRun()
        {
            _blocked = true;
        }

        private void CooldownControllerOnOnComplete()
        {
            _blocked = false;
        }

        void IAbility.Dispose()
        {
            if (_cooldownController)
            {
                _cooldownController.OnRun -= CooldownControllerOnOnRun;
                _cooldownController.OnComplete -= CooldownControllerOnOnComplete;
            }
        }
        
    }

}