using Player;
using UnityEngine;

namespace Events.MessageSystem.Messages
{
    public struct SendCreatePlayer_Msg : IMessage
    {
        public PlayerController controller;

        public SendCreatePlayer_Msg(PlayerController playerController)
        {
            Debug.Log("SEND");
            controller = playerController;
        }
    }
}