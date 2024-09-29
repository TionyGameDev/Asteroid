using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    public class AbilityController : MonoBehaviour
    { 
        private Ability [] _abilities;
        private void Awake()
        {
            _abilities = GetComponentsInChildren<Ability>();
            for (int i = 0; i < _abilities.Length; i++)
                (_abilities[i] as IAbility).Init();
        }

        public Ability GetAbility(Ability ability)
        {
            for (int i = 0; i < _abilities.Length; i++)
                if (_abilities[i] == ability)
                    return _abilities[i];

            return null;
        }
    }
}