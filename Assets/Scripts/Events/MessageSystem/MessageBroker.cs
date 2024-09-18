namespace Events.MessageSystem
{
    public static partial class MessageBroker
    {
        public const int broadcastChannelId = 0;


        public static readonly MessageBusManager buses;

        private static MessageBus _localBus;
        public static MessageBus localBus { get { return _localBus; } }
        static MessageBroker()
        {
            buses = new MessageBusManager();
            _localBus = buses.AddBus<LocalBus>();
        }
    }
    
    public class LocalBus : MessageBus
    {
        public LocalBus() : base(new LocalTransport()) { }        
    }
}