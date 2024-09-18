using System;
using Input;
using Player;
using PropertySystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Ability
{
    public interface IAbility
    {
        void Init();
        void Dispose();
    }

    public abstract class Ability : SerializedMonoBehaviour, IAbility
    {
        [SerializeField] 
        private CooldownController _cooldownController;

        [SerializeReference,OdinSerialize] 
        private ICooldownHandler _cooldownHandler = new NoneCooldownHandler();
        private bool useCooldown => !(_cooldownHandler is NoneCooldownHandler);

        [SerializeField,ShowIf(nameof(useCooldown))] 
        private float _cooldownTime;
        [SerializeField] 
        protected InputPlayer _inputPlayer;
        
        protected PropertyCharacter _character;
        protected Vector2 _input;
        protected Rigidbody2D _rigidbody;
        protected Transform _root;
        protected bool _blocked;
        protected Action _onActive;
        void IAbility.Init()
        {
            _root = transform.root;
            _character = _root.GetComponent<PropertyCharacter>();
            _rigidbody =  _root.GetComponent<Rigidbody2D>();
            
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