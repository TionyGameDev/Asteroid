using System;
using UnityEngine;

namespace PropertySystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterEntity : PropertyCharacter
    {
        [SerializeField] 
        protected Rigidbody2D _rigidbody;

        private void Awake()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}