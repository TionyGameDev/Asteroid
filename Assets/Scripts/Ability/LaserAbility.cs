
namespace Ability
{
    public class LaserAbility : ShootAbility
    {
        protected override void HandleShooting()
        {
            _onActive?.Invoke();
            
            _impact.ImpactInfo.attacker = _character;
            _weapon.InstantiateBullet(_impact,_bullet);
        }
    }
}