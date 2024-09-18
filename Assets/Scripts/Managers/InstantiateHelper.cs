using Singleton;
using UnityEngine;

namespace Managers
{
    public class InstantiateHelper : Singleton<InstantiateHelper>
    {
        public GameObject Create(GameObject gameObj,Vector3 position,Quaternion quaternion)
        {
            return Instantiate(gameObj, position, quaternion);
        }
    }
}