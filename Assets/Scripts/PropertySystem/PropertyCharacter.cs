using System;
using System.Collections.Generic;
using System.Linq;
using Events.MessageSystem;
using Events.MessageSystem.Messages;
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
        private List<Property> _characterProperties;

        private TagComponent _tagComponent;
        public Action<Tag> onAdd { get; set; }
        public Action<Tag> onRemove { get; set; }

        private readonly HashSet<Tag> tags = new HashSet<Tag>();

        private void OnEnable()
        {
            for (int i = 0; i < _characterProperties.Count; i++)
                _characterProperties[i].Init();
        }

        public Property GetProperty(PropertyName namePropety)
        {
            for (int i = 0; i < _characterProperties.Count; i++)
                if (_characterProperties[i].name == namePropety)
                    return _characterProperties[i];
                
            return null;
        }
        
        public void AddTag(Tag tag)
        {
            if (!tags.Contains(tag))
            {
                tags.Add(tag);
                onAdd?.Invoke(tag);
            }
            if (tag == Tag.Die)
            {
                MessageBroker.localBus.broadcastChannel.SendMessage(new SendDeadCharacter_Msg(this.transform));
                OnDie();
            }
        }

        protected virtual void OnDie() { }

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