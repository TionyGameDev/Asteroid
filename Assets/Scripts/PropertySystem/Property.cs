using System;
using UnityEngine;

namespace PropertySystem
{
    public enum PropertyName
    {
        Health,
        Shield
    }
    [Serializable]
    public class Property
    {
        public PropertyName name;
        
        public float currentValue;
        public float maxValue;

        public void Init()
        {
            currentValue = maxValue;
        }
    }
}