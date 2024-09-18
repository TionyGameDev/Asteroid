using Events.MessageSystem.Messages;

namespace Events.MessageSystem
{
    public interface IMessageBus
    {
        bool enable { get; set; }
        void SendMessage<T>(T message, int channelId) where T : struct, IMessage;
        void ReciveMessage<T>(T message, int channelId) where T : struct, IMessage;
    }
}