using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TagSystem
{
    [Serializable]
    public class TagEvent
    {
        public Tag tag;
        public UnityEvent eventsAdd;
        public UnityEvent eventsRemove;
    }

    [Serializable]
    public enum Tag
    {
        Die,
        Spawn
    }
}