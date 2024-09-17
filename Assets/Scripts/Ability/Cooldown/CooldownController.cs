using System;
using UnityEngine;

namespace Ability
{
    public class CooldownController : MonoBehaviour , ICooldown
    {
        [SerializeReference]
        private ICooldownHandler _cooldownHandler;

        [SerializeField]
        private float _currPoints;
        [SerializeField]
        private float _totalPoints;

        public event Action OnRun;
        public event Action OnComplete;
        public event Action<float> OnUpdate;

        public void Run()
        {
            OnRun?.Invoke();
            _cooldownHandler.Run();
        }

        public void Run(float total)
        {
            ((ICooldown) this).SetPoints(total);
            Run();
        }

        void ICooldown.Complete()
        {
            OnComplete?.Invoke();
            
            _cooldownHandler.Complete();
        }
        private void AddPoints(float value)
        {
            if (value == 0) return;
            
            _currPoints = _currPoints + value;

            _currPoints = _currPoints > _totalPoints ? _totalPoints : _currPoints;

            OnUpdate?.Invoke(_currPoints);

            if (VerifyOnComplete())
                ((ICooldown) this).Complete();
        }

        private bool VerifyOnComplete() => _currPoints >= _totalPoints;

        void ICooldown.SetPoints(float points)
        {
            _totalPoints = points;
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

        void IDisposable.Dispose()
        {
            _cooldownHandler?.Dispose();
        }
    }
}