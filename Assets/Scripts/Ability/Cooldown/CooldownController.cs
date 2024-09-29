using System;
using Sirenix.Serialization;
using UnityEngine;

namespace Ability.Cooldown
{
    public class CooldownController : MonoBehaviour, ICooldown
    {
        [SerializeReference]
        private ICooldownHandler _cooldownHandler;
        
        [SerializeReference, OdinSerialize]
        private IVerifyHandler _verifyHandler;

        [SerializeField]
        private float _currentCooldown;
        
        [SerializeField]
        private float _maxCooldown;
        
        private float _perSec;

        public event Action OnRun;
        public event Action OnComplete;
        public event Action<float> OnUpdate;

        private void OnDisable()
        {
            Dispose();
        }

        public void Run(float perSec)
        {
            OnRun?.Invoke();
            _cooldownHandler?.Run(perSec);
        }

        public void Run(float perSec, float total)
        {
            SetPoints(perSec, total);
            Run(perSec);
            
            _currentCooldown = Mathf.Max(_currentCooldown - 1, 0);
        }

        public void Complete()
        {
            OnComplete?.Invoke();
            _cooldownHandler?.Complete();
        }

        private void AddPoints(float value)
        {
            if (value == 0) return; 

            _currentCooldown = Mathf.Min(_currentCooldown + value, _maxCooldown);

            OnUpdate?.Invoke(_currentCooldown);

            if (_verifyHandler?.VerifyOnComplete(_currentCooldown, _maxCooldown) == true)
            {
                Complete();
            }
        }

        public void SetPoints(float perSec, float points)
        {
            _maxCooldown = points;
            _perSec = perSec;
        }

        public void SetHandler(ICooldownHandler handler)
        {
            _cooldownHandler = handler;
            _cooldownHandler.Init(this);
            _cooldownHandler.OnAddPoint += CooldownHandlerOnOnAddPoint;
        }

        private void CooldownHandlerOnOnAddPoint(float value)
        {
            AddPoints(value);
        }

        public void Dispose()
        {
            if (_cooldownHandler != null)
            {
                _cooldownHandler.OnAddPoint -= CooldownHandlerOnOnAddPoint;
                _cooldownHandler.Dispose();
            }
        }
    }
}
