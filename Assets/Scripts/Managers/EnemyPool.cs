using System;
using GameSystem;
using Player;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class EnemyPool : PoolObjects<BaseEnemy>
    {
        private void Start()
        {
            poolList.Clear();
        }

        public GameObject ReturnEnemyData(TypeEnemy ID)
        {
            for (int i = 0; i < poolList.Count; i++)
            {
                var mob = poolList[i];
                if (mob != null && mob.typeEnemy == ID && !mob.gameObject.activeInHierarchy)
                    return mob.gameObject;
            }
            
            return null;
        }
        
        public void DisableAll()
        {
          //i//f (poolList[i].transform != null)
                   // poolList[i].gameObject.GetComponent<Destroyer>().DestroySelf();
        }

        public void DisableEnemy(GameObject enemy)
        {
        }
    }
}