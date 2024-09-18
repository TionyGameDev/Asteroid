using System;
using System.Collections.Generic;
using Events.MessageSystem.Messages;

namespace Events.MessageSystem
{
    public class Unsubscriber : IDisposable
    {
        private IObserver<IMessage> _observer;
        private List<IObserver<IMessage>> _observers;
        public Unsubscriber(IObserver<IMessage> observer, List<IObserver<IMessage>> observers)
        {
            _observer = observer;
            _observers = observers;
        }

        void IDisposable.Dispose()
        {
            _observers.Remove(_observer);
        }
    }
}