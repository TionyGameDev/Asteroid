using System;
using Bullets;
using Input;
using Player;
using PropertySystem;
using UnityEngine;

namespace Ability
{
    public class ShootAbility : Ability
    {
        public enum Events
        { 
            OnShoot,
            OnStay
        }
        
        [SerializeField] 
        protected ImpactSetting _impact;
        [SerializeField] 
        protected Bullet _bullet;
        
        [SerializeField] 
        protected Weapon _weapon;

        protected bool _press;

        private void Start()
        {
            _inputPlayer.OnPressChanged += InputPlayerOnPressChanged;
        }

        private void InputPlayerOnPressChanged(bool obj)
        {
            _press = obj;
        }

        private void Update()
        {
            if (_press && !_blocked)
                HandleShooting();
        }

        protected virtual void HandleShooting() 
        {
            _onActive?.Invoke();
            
            Quaternion rotationShip =_root.rotation;
            
            _impact.ImpactInfo.attacker = _character;
            _weapon.InstantiateBullet(_impact,_bullet,rotationShip);

        }

    }
}
