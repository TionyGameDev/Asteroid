using System;
using System.Collections.Generic;
using System.Linq;
using Events.MessageSystem.Messages;

namespace Events.MessageSystem
{
    public class MessageBus : IMessageBus
        , IObservable<IMessage>
    {
        public readonly MessageChannelManager channels;

        public readonly MessageChannel broadcastChannel;

        private List<IMessage> _messagePool = new List<IMessage>();
        private List<IObserver<IMessage>> _observers = new List<IObserver<IMessage>>();

        public bool enable { get; set; } = true;

        private ITransporter _transporter;

        public MessageBus(ITransporter transporter)
        {
            _transporter = transporter;
            _transporter.bus = this;
            channels = new MessageChannelManager(this);
            broadcastChannel = new BroadcastChannel(this);  
            channels.AddChannel(broadcastChannel);
        }

        protected void SendMessage<T>(T message, int channelId) where T : struct, IMessage
        {
            _transporter.Send(message, channelId);
        }

        void IMessageBus.ReciveMessage<T>(T message, int channelId)
        {
            IMessageChannel channel = channels[channelId];

            if (channel != null)
                channel.ReciveMessage(message);

            _messagePool.Add(message);
            
            var observers = _observers.ToList();
            for (int i = 0; i < observers.Count; i++)
            {
                var observer = observers[i];
                observer.OnNext(message);
            }
        }


        void IMessageBus.SendMessage<T>(T message, int channelId)
        {
            if (enable)
                SendMessage(message, channelId);
        }

        IDisposable IObservable<IMessage>.Subscribe(IObserver<IMessage> observer)
        {
            if (_observers.Contains(observer)) return new Unsubscriber(observer, _observers);

            _observers.Add(observer);

            for (int i = 0; i < _messagePool.Count; i++)
            {
                var msg = _messagePool[i];
                observer.OnNext(msg);
            }

            return new Unsubscriber(observer, _observers);
        }
    }
}
