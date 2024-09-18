using UnityEngine;

namespace Events.MessageSystem
{
    public class BroadcastChannel : MessageChannel
    {
        public BroadcastChannel(IMessageBus messageBus) : base(MessageBroker.broadcastChannelId, messageBus)
        {
        }
    }
}