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
        [SerializeField] 
        private ImpactSetting _impact;
        [SerializeField] 
        private Bullet _bulletNew;
        
        [SerializeField] 
        private Weapon _weapon;

        private bool _press;

        private void Update()
        {
            _press = _inputPlayer.ReadPress();
            if (_press && !_blocked)
                HandleShooting();
        }

        private void HandleShooting() 
        {
            _onActive?.Invoke();
            
            Quaternion rotationShip =_root.rotation;

            var impact = new ImpactSetting();
            _impact.ImpactInfo.attacker = _character;
            _weapon.InstantiateBullet(_impact,_bulletNew,rotationShip);

        }

    }
}
