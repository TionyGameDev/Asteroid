using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public static class ResourcesLoader
    {
        public static GameObject LoadPrefab(string path)
        {
            //LogWrapper.LogError("Load prefab: " + path);

            GameObject cashedPrefab = CashedPrefab(path);
            if (cashedPrefab != null)
                return cashedPrefab;

            return Load<GameObject>(path);
        }
        private static readonly Dictionary<string, GameObject> _cashedPrefabs = new Dictionary<string, GameObject>();

        private static GameObject CashedPrefab(string path)
        {
            if (_cashedPrefabs.ContainsKey(path))
                return _cashedPrefabs[path];
            return null;
        }
        
        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            if (path == string.Empty)
                return null;

            T res = null;
            
            if (res == null)
                res = LoadFromResources<T>(path);

            return res;
        }
        
        public static T LoadFromResources<T>(string path) where T : UnityEngine.Object
        {
            T obj = (T)Resources.Load<T>(path);
            
            return obj;
        }
    }
}