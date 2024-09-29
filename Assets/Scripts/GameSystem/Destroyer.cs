using System;
using System.Collections;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    interface IDestroyer
    {
        void UseDestroy();
    }
    public class Destroyer : MonoBehaviour , IDestroyer
    {
        [SerializeField] 
        private UnityEvent _beforeAction;
        
        [SerializeField] 
        private float _delay;
        
        [SerializeField] 
        private bool _autoDestroy;
        [SerializeField, ShowIf(nameof(_autoDestroy))] 
        private float _timeAutoDestroy;

        private void OnEnable()
        {
            if (_autoDestroy)
                StartCoroutine(nameof(DelayDestroy));// DelayDestroy();
        }

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(_timeAutoDestroy);
            
            ((IDestroyer) this).UseDestroy();
        }

        public void DestroySelf()
        {
            if (_autoDestroy)
                StopCoroutine(nameof(DelayDestroy));
            
            ((IDestroyer) this).UseDestroy();
        }

        void IDestroyer.UseDestroy()
        {
            if (this == null)
                return;
            
            if (gameObject.activeSelf)
                StartCoroutine(nameof(SelfDestroy));
        }

        private IEnumerator SelfDestroy()
        {
            _beforeAction?.Invoke();
            
            yield return new WaitForSeconds(_delay);
            Destroy(gameObject);
            
        }
    }
}