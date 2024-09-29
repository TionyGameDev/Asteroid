using Ability;
using UnityEngine;

namespace UI
{
    public abstract class BaseUIAbility : MonoBehaviour
    {
        public abstract void Init(IAbility ability);
    }
}