using System;
using Autofac;
using Apps.Communication.Core;
using System.Collections.Generic;

namespace Cdln.School.People.Uwp
{
    public sealed class EventHub : IEventHub
    {
        private void OnHandlingException(Exception ex)
        {
            throw new Exception($"An exception was encountered while handling the event. \n\n {ex.Message}", ex.InnerException);
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IEvent
        {
            Type key = typeof(TEvent);

            if (RegisteredHandlers.ContainsKey(key))
            {
                HashSet<WeakReference> handlerReferences = new HashSet<WeakReference>();
                foreach (var handlerType in RegisteredHandlers[key])
                {
                    if (App.Container.Resolve(handlerType) is IHandle<TEvent> handler)
                    {
                        // handler.Handle(@event).FireAndForget(false, OnHandlingException);
                        handlerReferences.Add(new WeakReference(handler));
                    }
                }
                if (handlerReferences.Count > 0)
                {
                    if (LiveHandlersReferences.ContainsKey(key))
                    {
                        foreach (var h in handlerReferences)
                        {
                            LiveHandlersReferences[key].Add(h);
                        }
                    }
                    else { LiveHandlersReferences.Add(key, handlerReferences); }
                }
                // get rid of that key!
                RegisteredHandlers.Remove(key);
            }
            if (LiveHandlersReferences.ContainsKey(key))
            {
                foreach (var reference in LiveHandlersReferences[key])
                {
                    if (reference.IsAlive && reference.Target is IHandle<TEvent> handler)
                    {
                        handler.Handle(@event).FireAndForget(false, OnHandlingException);
                    }
                }
            }
        }

        public void RegisterHandler<THandler, TEvent>(THandler handler) where TEvent : IEvent where THandler : IHandle<TEvent>
        {
            Type eventType = typeof(TEvent);
            WeakReference handlerReference = new WeakReference(handler);
            if (LiveHandlersReferences.ContainsKey(eventType))
            {
                if (!LiveHandlersReferences[eventType].Contains(handlerReference))
                { LiveHandlersReferences[eventType].Add(handlerReference); }
            }
            else
            {
                HashSet<WeakReference> references = new HashSet<WeakReference>() { handlerReference };
                LiveHandlersReferences.Add(eventType, references);
            }
        }

        public void RegisterHandler<THandler, TEvent>() where TEvent : IEvent where THandler : IHandle<TEvent>
        {
            Type eventType = typeof(TEvent);
            Type handlerType = typeof(THandler);

            if (RegisteredHandlers.ContainsKey(eventType))
            {
                if (!RegisteredHandlers[eventType].Contains(handlerType))
                { RegisteredHandlers[eventType].Add(handlerType); }
            }
            else
            {
                List<Type> handlersList = new List<Type>() { handlerType };
                RegisteredHandlers.Add(eventType, handlersList);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    RegisteredHandlers.Clear();
                    LiveHandlersReferences.Clear();
                }
                disposed = true;
            }
        }

        private readonly Dictionary<Type, HashSet<WeakReference>> LiveHandlersReferences = new Dictionary<Type, HashSet<WeakReference>>();
        private readonly Dictionary<Type, List<Type>> RegisteredHandlers = new Dictionary<Type, List<Type>>();

        // disposed flag
        private bool disposed = false;
    }
}
