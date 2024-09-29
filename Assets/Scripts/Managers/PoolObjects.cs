using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    [Serializable]
    public class PoolObjects<T> 
    { 
        public List<T> poolList = new List<T>();

        public void AddToPool(T obj)
        {
            if (!poolList.Contains(obj))
                poolList.Add(obj);
        }

        public void RemoveToPool(T obj)
        {
            if (poolList.Contains(obj))
                poolList.Remove(obj);
        }

        public List<T> ReturnPool()
        {
            if (poolList.Count > 0)
                return poolList;
            
            return null;
        }

    }
}