using System;
using Ability;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UILaserAbility : BaseUIAbility
    {
        [SerializeField] private TextMeshProUGUI _chargeText;
        [SerializeField] private TextMeshProUGUI _changeCooldownText;
        private LaserAbility _laserAbility;
        public override void Init(IAbility ability)
        {
            _laserAbility = ability as LaserAbility;
            if (_laserAbility)
            {
                _laserAbility.сhargeValueEvent += ChangeValueEvent;
                _laserAbility.сhangeCooldownValueEvent += ChangeCooldownValueEvent;
            }
        }

        private void OnDestroy()
        {
            if (_laserAbility)
            {
                _laserAbility.сhargeValueEvent -= ChangeValueEvent;
                _laserAbility.сhangeCooldownValueEvent -= ChangeCooldownValueEvent;
            }
        }

        private void ChangeCooldownValueEvent(float obj)
        {
            if (_changeCooldownText)
                _changeCooldownText.text = string.Format("Cooldown - {0}",obj.ToString("F1"));
        }

        private void ChangeValueEvent(int obj)
        {
            if (_chargeText)
                _chargeText.text = string.Format("Charge -{0}",obj);
        }
    }
}