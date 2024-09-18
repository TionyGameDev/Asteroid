using Events.MessageSystem.Messages;

namespace Events.MessageSystem
{
    public interface ITransporter
    {
        IMessageBus bus { get; set; }
        void Send<T>(T message, int channelId) where T : struct, IMessage;
        void Recive<T>(T message, int channelId) where T : struct, IMessage;
    }
    
    
    public class LocalTransport : ITransporter
    {
        public IMessageBus bus { get; set; }

        public void Recive<T>(T message, int channelId) where T : struct, IMessage
        {
            bus.ReciveMessage(message, channelId);
        }

        public void Send<T>(T message, int channelId) where T : struct, IMessage
        {
            Recive(message, channelId);
        }
    }
   
        
    
}