﻿using System;
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
    }
}