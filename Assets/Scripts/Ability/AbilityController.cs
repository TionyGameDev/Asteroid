using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    public class AbilityController : MonoBehaviour
    {
        [SerializeReference] private IAbility [] _abilities;

        private void Awake()
        {
            _abilities = GetComponentsInChildren<IAbility>();
            for (int i = 0; i < _abilities.Length; i++)
                _abilities[i].Init();   
            
        }
    }
}