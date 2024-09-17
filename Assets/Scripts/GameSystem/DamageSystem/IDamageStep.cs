using System;
using PropertySystem;
using UnityEngine;

namespace GameSystem.DamageSystem
{
    public interface IDamageStep
    {
        void Execute(ImpactInfo data, PropertyCharacter entity, Action next);
    }

}


   