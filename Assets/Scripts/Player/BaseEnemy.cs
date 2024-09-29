using Managers;
using PropertySystem;
using UnityEngine;

namespace Player
{
    public class BaseEnemy : CharacterEntity
    {
        [SerializeField] private TypeEnemy _typeEnemy;
        public TypeEnemy typeEnemy => _typeEnemy;
    }
}