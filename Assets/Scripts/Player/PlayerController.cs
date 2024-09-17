using GameSystem.DamageSystem;
using Managers;
using PropertySystem;
using UnityEngine;

namespace Player
{
    public class PlayerController : PropertyCharacter
    {
        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            //ScreenWrapManager.Instance.SetPlayer(this.gameObject);
        }
    }
}