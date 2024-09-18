using System;
using System.Collections.Generic;

namespace Events.MessageSystem
{
    public class MessageChannelManager
    {
        private class Channels
        {
            private Dictionary<Type, Dictionary<int, MessageChannel>> _channelsByType;
            private Dictionary<int, MessageChannel> _channelsById;

            public Channels()
            {
                _channelsByType = new Dictionary<Type, Dictionary<int, MessageChannel>>();
                _channelsById = new Dictionary<int, MessageChannel>();
            }
            public void AddChannel(MessageChannel channel)
            {
                if (channel == null || GetChannel(channel.id) != null) return;

                var channelType = channel.GetType();
                if (!_channelsByType.ContainsKey(channelType))
                    _channelsByType.Add(channelType, new Dictionary<int, MessageChannel>()); ;

                _channelsByType[channelType].Add(channel.id, channel);
                _channelsById.Add(channel.id, channel);
            }

            public void RemoveChannel(MessageChannel channel)
            {
                if (channel == null || GetChannel(channel.id) == null) return;

                var channelType = channel.GetType();
   
                _channelsByType[channelType].Remove(channel.id);
                _channelsById.Remove(channel.id);
            }

            public void RemoveChannel(int channelId)
            {
                RemoveChannel(GetChannel(channelId));
            }

            public MessageChannel GetChannel(int channelId)
            {
                MessageChannel channel;

                if (_channelsById.TryGetValue(channelId, out channel))
                    return channel;

                return null;
            }

            public T GetChannel<T>(int channelId) where T : MessageChannel
            {
                Dictionary<int, MessageChannel> channels;
                if (_channelsByType.TryGetValue(typeof(T), out channels))
                {
                    MessageChannel channel;
                    if (channels.TryGetValue(channelId, out channel))
                        return channel as T;
                }

                return null;
            }

            public int GetFreeChannelId()
            {
                int channelId = new Random().Next(0, int.MaxValue);

                while (_channelsById.ContainsKey(channelId))
                    channelId = new Random().Next(0, int.MaxValue);

                return channelId;
            }
        }

        private Channels _channels;
        private IMessageBus _messageBus;
        public MessageChannelManager(IMessageBus messageBus)
        {
            _channels = new Channels();
            _messageBus = messageBus;
        }

        public void AddChannel(MessageChannel channel)
        {
            _channels.AddChannel(channel);
        }

        public MessageChannel AddChannel(int id)
        {
            var channel = new MessageChannel(id, _messageBus);
            _channels.AddChannel(channel);

            return channel;
        }

        public T AddChannel<T>(int id) where T : MessageChannel
        {
            var channel = Activator.CreateInstance(typeof(T), id, _messageBus) as T;
            _channels.AddChannel(channel);
            return channel;
        }

        public T AddChannel<T>() where T : MessageChannel
        {
            return AddChannel<T>(_channels.GetFreeChannelId());
        }

        public void RemoveChannel(int channelId)
        {
            _channels.RemoveChannel(channelId);
        }

        public MessageChannel GetChannel(int channelId)
        {
            return _channels.GetChannel(channelId);
        }

        public MessageChannel this[int channelId]
        {
            get { return GetChannel(channelId); }
        }
    }
}
