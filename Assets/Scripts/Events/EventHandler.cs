using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public static class EventHandler 
    {
        private class EventListener
        {
            public Delegate action;
            public object obj;

            public EventListener(Delegate action, object obj)
            {
                this.action = action;
                this.obj = obj;
            }
            public EventListener(Delegate action) : this(action, null)
            {

            }
        }

        private static Dictionary<string, List<EventListener>> _listeners = new Dictionary<string, List<EventListener>>();

        
        public static void ExecuteEvent(string eventName)
        {
            ExecuteEventInternal(null, eventName, (d) => d.DynamicInvoke());
        }
        private static void ExecuteEventInternal(object obj, string eventName, Action<Delegate> invocker)
        {
            if (_listeners.ContainsKey(eventName))
            {
                var listeners = _listeners[eventName];
                for (var i = 0; i < listeners.Count; i++)
                {
                    var listener = listeners[i];

                    if (listener.obj == obj)
                    {
                        invocker?.Invoke(listener.action);
                    }
                }
            }
        }

        #region Regist

        public static void RegisterEvent(string eventName, Action action)
        {
            RegisterEventInternal(null, eventName, action);
        }
        public static void RegisterEvent<T1>(string eventName, Action<T1> action)
        {
            RegisterEventInternal(null, eventName, action);
        }
        private static void RegisterEventInternal(object obj, string eventName, Delegate action)
        {
            if (_listeners.TryGetValue(eventName, out var listeners))
            {
                for (var i = 0; i < listeners.Count; i++)
                {
                    var listener = listeners[i];
                    if (listener.action.Target == action.Target && listener.obj == obj)
                        return;
                }
            }
            else
            {
                listeners = new List<EventListener>();
                _listeners.Add(eventName, listeners);
            }
            
            listeners.Add(new EventListener(action, obj));
        }

        #endregion
        

        #region UnRegist

        public static void UnregisterEvent( string eventName, Action action)
        {
            UnregisterEventInternal(null, eventName, action);
        }
        private static void UnregisterEventInternal(object obj, string eventName, Delegate action)
        {
            if (_listeners.TryGetValue(eventName, out var listeners))
            {
                for (var i = 0; i < listeners.Count; i++)
                {
                    var listener = listeners[i];
                    if (listener.action.Target == action.Target && listener.obj == obj)
                    {
                        listeners.Remove(listener);
                        return;
                    }
                }
            }
        }

        #endregion
       
    }
}