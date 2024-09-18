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
                DelayDestroy();
        }

        public void DestroySelf()
        {
            ((IDestroyer) this).UseDestroy();
        }

        private async void DelayDestroy()
        {
            await Task.Delay((int)_timeAutoDestroy * 1000);
            if (gameObject)
                ((IDestroyer) this).UseDestroy();
        }

        void IDestroyer.UseDestroy()
        {
            StartCoroutine(nameof(SelfDestroy));
        }

        private IEnumerator SelfDestroy()
        {
            _beforeAction?.Invoke();
            
            yield return new WaitForSeconds(_delay);
            
            Destroy(this.gameObject);
            
        }
    }
}