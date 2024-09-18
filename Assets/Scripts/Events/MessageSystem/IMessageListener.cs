using System;
using Events.MessageSystem.Messages;

namespace Events.MessageSystem
{
    public interface IMessageChannel
    {
        void SendMessage<T>(T message) where T : struct, IMessage;
        void SendMessage<T>() where T : struct, IMessage;

        void ReciveMessage<T>(T message) where T : IMessage;
        void Subscribe(IMessageListener messageListener);
        void Unsubscribe(IMessageListener messageListener);
    }
    
    public interface IMessageListener
    {
    }
    public interface IMessageListener<T> : IMessageListener where T : IMessage
    {
        void OnMessage(T message);
    }
    
    
       
    

}