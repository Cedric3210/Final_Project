using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class EventDispatcher
    {
        private static readonly EventDispatcher instance = new EventDispatcher();

        public static EventDispatcher Instance => instance;

        private readonly Dictionary<Type, List<Delegate>> _eventHandlers = new Dictionary<Type, List<Delegate>>();

        private EventDispatcher() { }

        public void Subscribe<T>(Action<T> handler) where T : EventArgs
        {
            if (!_eventHandlers.TryGetValue(typeof(T), out var handlers))
            {
                handlers = new List<Delegate>();
                _eventHandlers[typeof(T)] = handlers;
            }
            handlers.Add(handler);
        }

        public void Publish<T>(T eventArgs) where T : EventArgs
        {
            if (_eventHandlers.TryGetValue(typeof(T), out var handlers))
            {
                foreach (var handler in handlers.OfType<Action<T>>())
                {
                    handler.Invoke(eventArgs);
                }
            }
        }
    }

}
