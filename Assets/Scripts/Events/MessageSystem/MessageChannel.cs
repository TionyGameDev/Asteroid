using System;
using System.Collections.Generic;
using Events.MessageSystem.Messages;

namespace Events.MessageSystem
{
    public class MessageChannel : IMessageChannel
    {
        public int id { get; private set; }

        private List<IMessageListener> _listeners;
        private Dictionary<Type, List<IMessageListener>> _cach;
        private Dictionary<Delegate, IMessageListener> _callbacks = new Dictionary<Delegate, IMessageListener>();


        private IMessageBus _bus;
        public bool enable { get; set; } = true;

        public MessageChannel(int id, IMessageBus bus)
        {
            this.id = id;
            _listeners = new List<IMessageListener>();
            _cach = new Dictionary<Type, List<IMessageListener>>();
            _bus = bus;
        }

        public void SendMessage<T>(T message) where T : struct, IMessage
        {
            if (enable)
                _bus.SendMessage(message, id);
        }

        public void SendMessage<T>() where T : struct, IMessage
        {
            SendMessage(new T());
        }
        
        void IMessageChannel.ReciveMessage<T>(T message)
        {
            var selectedListeners = new List<IMessageListener<T>>();
            for (int i = 0; i < _listeners.Count; i++)
            {
                var listener = _listeners[i];
                if (listener == null || !(listener is IMessageListener<T> tListener))
                    continue;
                selectedListeners.Add(tListener);
            }

            for (int i = 0; i < selectedListeners.Count; i++)
            {
                var tListener = selectedListeners[i];
                //try
                {
                    tListener.OnMessage(message);
                }
                //catch (Exception ex)
                //{
                //    //Debug.LogErrorFormat("Exception OnMessage {0}\n{1}", ex.Message, ex.StackTrace);
                //}
            }
        }

        public void Subscribe(IMessageListener messageListener)
        {
            if (_listeners.Contains(messageListener)) return;

            _listeners.Add(messageListener);
        }

        public void Unsubscribe(IMessageListener messageListener)
        {
            if (!_listeners.Contains(messageListener)) return;

            _listeners.Remove(messageListener);
        }

        public void UnregisterMessageCallback<T>(Action<T> callback) where T : IMessage
        {
            if (!_callbacks.ContainsKey(callback)) return;

            (this as IMessageChannel).Unsubscribe(_callbacks[callback]);

            _callbacks.Remove(callback);
        }
    }
}
