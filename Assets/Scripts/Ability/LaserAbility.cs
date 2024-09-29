using System;
using System.Collections;
using System.Threading.Tasks;
using Player;
using PropertySystem;
using UnityEngine;

namespace Ability
{
    public class LaserAbility : Ability
    {
        [SerializeField] 
        private ImpactSetting _impact;

        [SerializeField]
        private Weapon _weapon;
        [SerializeField] 
        private float _distance;

        [SerializeField] 
        private float _duration = 1;

        [SerializeField] 
        private LineRenderer _line;

        [SerializeField] 
        private int _chargeMax = 2;

        public Action<int> сhargeValueEvent { get; set; }
        public Action<float> сhangeCooldownValueEvent { get; set; }

        private int _charge = 1;

        private bool _isActiveLaser;
        protected bool _press;

        private void Start()
        {
            SetLineSetting();

            сhargeValueEvent?.Invoke(_charge);
            _inputPlayer.OnPressChanged += InputPlayerOnPressChanged;
        }

        private void SetLineSetting()
        {
            _line.startWidth = 0.05f;
            _line.endWidth = 0.05f;
            _line.startColor = Color.yellow;
            _line.endColor = Color.red;
            _line.enabled = false;
        }

        private void InputPlayerOnPressChanged(bool obj)
        {
            _press = obj;
        }

        private void Update()
        {
            if (_press && !_isActiveLaser && _charge > 0)
                ShootLaserAsync();
            
        }
        private async Task ShootLaserAsync()
        {
            Debug.Log("ShootLaser");
            _charge -= 1;
            _isActiveLaser = true;
            _line.enabled = true;
    
            var duration = _duration;

            while (duration > 0)
            {
                var position = _weapon.point.position;
                RaycastHit2D[] hits = Physics2D.RaycastAll(position, transform.root.up, _distance, _impact.layerMask.value);
                _line.SetPosition(0, _weapon.point.position);

                for (int i = 0; i < hits.Length; i++)
                {
                    var impactable = hits[i].transform.GetComponent<IImpactable>();
                    if (impactable != null)
                    {
                        _impact.ImpactInfo.attacker = transform.root.GetComponent<PropertyCharacter>();
                        _impact.ImpactInfo.target = impactable as PropertyCharacter;
                        impactable.Apply(_impact.ImpactInfo);
                    }
                }

                _line.SetPosition(1, (_weapon.point.up) * _distance);
                
                await Task.Yield();

                duration -= Time.deltaTime;
            }

            _line.enabled = false;
            _isActiveLaser = false;
            _onActive?.Invoke();
            
        }

        protected override void OnUpdateCooldown(in float value)
        {
            base.OnUpdateCooldown(in value);
            if (_cooldownTime <= 0) 
                return;
            
            _charge = (int) ((int) value * _chargeMax / _cooldownTime);

            var procent = (value / _cooldownTime) * 100;
            сhangeCooldownValueEvent?.Invoke(procent);
            сhargeValueEvent?.Invoke(_charge);
        }
    }
}