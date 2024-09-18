using UnityEngine;

namespace Events.MessageSystem.Messages
{
    public struct SendNewPosition_Msg : IMessage
    {
        public Vector2 position;

        public SendNewPosition_Msg(Vector2 pos)
        {
            position = pos;
        }
    }
}