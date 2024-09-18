using System;
using Events.MessageSystem;
using Events.MessageSystem.Messages;
using Managers;
using Player;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystem.GameState.Step
{
    public class InstantiateStep : IStepInit
    {
        private static string path = "Prefabs/Player";
        public void Execute(Action next)
        {
            var gameObject =  ResourcesLoader.LoadPrefab(path);
            if (gameObject)
            {
                var player = InstantiateHelper.Instance.Create(gameObject, Vector3.zero, quaternion.identity);
                if (player)
                    MessageBroker.localBus.broadcastChannel.SendMessage(new SendCreatePlayer_Msg(player.GetComponent<PlayerController>()));
            }
            
            next();
        }
    }
}