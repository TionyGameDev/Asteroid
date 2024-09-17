using System;
using System.Collections.Generic;
using System.Linq;
using GameSystem.DamageSystem;
using TagSystem;
using UnityEngine;

namespace PropertySystem
{
    [RequireComponent(typeof(TagComponent))]
    public abstract class PropertyCharacter : MonoBehaviour, IImpactable , ITaggable
    {
        [SerializeField]
        private DamageSystem _damageSystem;
        
        [SerializeField] 
        private List<Property> _propertiesCharacter;

        private TagComponent _tagComponent;
        public Action<Tag> onAdd { get; set; }
        public Action<Tag> onRemove { get; set; }

        private readonly HashSet<Tag> tags = new HashSet<Tag>();

        private void Start()
        {
            for (int i = 0; i < _propertiesCharacter.Count; i++)
                _propertiesCharacter[i].Init();
        }

        public Property GetProperty(PropertyName namePropety)
        {
            for (int i = 0; i < _propertiesCharacter.Count; i++)
                if (_propertiesCharacter[i].name == namePropety)
                    return _propertiesCharacter[i];
                
            return null;
        }
        
        public void AddTag(Tag tag)
        {
            if (!tags.Contains(tag))
            {
                tags.Add(tag);
                onAdd?.Invoke(tag);
            }
        }

        public void RemoveTag(Tag tag)
        {
            if (tags.Contains(tag))
            {
                tags.Remove(tag);
                onRemove?.Invoke(tag);
            }
            
        }

        public bool HasTag(Tag tag)
        {
            return tags.Contains(tag);
        }

        public IEnumerable<Tag> GetTags()
        {
            return tags;
        }
        
        public void Apply(ImpactInfo impactInfo)
        {
           _damageSystem.Apply(impactInfo);
        }
    }
}