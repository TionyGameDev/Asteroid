using Ability;
using GameSystem.DamageSystem;
using Managers;
using PropertySystem;
using UnityEngine;

namespace Player
{
    public class PlayerController : CharacterEntity
    {
        [SerializeField] 
        private AbilityController _abilityController;

        public AbilityController abilityController => _abilityController;
    }
}