using System;
using System.Collections.Generic;
using PropertySystem;
using Sirenix.OdinInspector;
using UnityEditor.PackageManager;
using UnityEngine;

namespace TagSystem
{
    public interface ITaggable
    {
        void AddTag(Tag tag);
        void RemoveTag(Tag tag);
        bool HasTag(Tag tag);
        IEnumerable<Tag> GetTags();
    }
    
    public class TagComponent : SerializedMonoBehaviour
    {
        private PropertyCharacter _propertyCharacter;
        [SerializeField] 
        private List<TagEvent> _eventTag;
        
        private void Awake()
        {
            _propertyCharacter = GetComponent<PropertyCharacter>();
            _propertyCharacter.onAdd += OnAddTag;
            _propertyCharacter.onRemove += OnRemoveTag;
        }

        private void OnAddTag(Tag tagObj)
        {
            for (int i = 0; i < _eventTag.Count; i++)
            {
                var tagEvent = _eventTag[i];    
                if (tagEvent.tag == tagObj)
                {
                    Debug.Log("INVOKE");
                    tagEvent.eventsAdd?.Invoke();
                }
                //if (tagEvent.tag.Contains(tagObj.ToString()))
                {
                    
                }
            }
        }
        
        private void OnRemoveTag(Tag tagObj)
        {
            
        }
    }
}