using UnityEngine;

namespace Events.MessageSystem.Messages
{
    public struct SendDeadCharacter_Msg : IMessage
    {
        public Transform targetDead;

        public SendDeadCharacter_Msg(Transform target)
        {
            targetDead = target;
        }
    }
}