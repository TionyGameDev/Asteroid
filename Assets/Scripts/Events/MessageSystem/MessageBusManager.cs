using System;
using System.Collections.Generic;

namespace Events.MessageSystem
{
    public class MessageBusManager
    {
        public event Action<MessageBus> onAddBus;
        public event Action<MessageBus> onRemoveBus;

        private Dictionary<Type, MessageBus> _buses;

        public MessageBusManager()
        {
            _buses = new Dictionary<Type, MessageBus>();
        }

        public void AddBus(MessageBus bus)
        {
            if (bus == null || _buses.ContainsKey(bus.GetType())) return;

            _buses.Add(bus.GetType(), bus);

            onAddBus?.Invoke(bus);
        }

        public T AddBus<T>() where T : MessageBus, new()
        {
            if (_buses.ContainsKey(typeof(T))) return GetBus<T>();

            var bus = new T();
            _buses.Add(typeof(T), bus);

            onAddBus?.Invoke(bus);

            return bus;
        }

        public void RemoveBus<T>() where T : MessageBus
        {
            var bus = GetBus<T>();

            if (bus != null)
            {
                _buses.Remove(typeof(T));

                onRemoveBus?.Invoke(bus);
            }
        }

        public MessageBus GetBus(Type busType)
        {
            MessageBus bus;

            if (_buses.TryGetValue(busType, out bus))
                return bus;
            return null;
        }
        public T GetBus<T>() where T : MessageBus
        {
            return GetBus(typeof(T)) as T;
        }
    }
}