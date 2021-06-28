using System;
using System.Threading;
using Apps.Communication.Core;
using System.Collections.Generic;

namespace Cdln.School.People.Uwp
{
    public sealed class MessageHub : IMessageHub, IDisposable
    {
        public void RegisterHandler<THandler, TMessage>(THandler handler)
            where TMessage : IMessage where THandler : IHandle<TMessage>
        {
            Type key = typeof(TMessage);

            SignalToHandlers.WaitOne();
            SignalToHandlers.Reset();

            if (Handlers.ContainsKey(key) == false) { Handlers.Add(key, new HashSet<object>()); }
            Handlers[key].Add(handler);

            SignalToHandlers.Set();
        }

        public void UnregisterHandler<THandler, TMessage>(THandler handler)
            where TMessage : IMessage where THandler : IHandle<TMessage>
        {
            Type key = typeof(TMessage);

            SignalToHandlers.WaitOne();
            SignalToHandlers.Reset();

            if (Handlers.ContainsKey(key))
            {
                if (Handlers[key].Contains(handler))
                {
                    Handlers[key].Remove(handler);
                    if (Handlers[key].Count == 0) { Handlers.Remove(key); }
                }
            }

            SignalToHandlers.Set();
        }

        public void Dispatch<TMessage>(TMessage message) where TMessage : IMessage
        {
            Type key = typeof(TMessage);

            SignalToHandlers.WaitOne();
            SignalToHandlers.Reset();

            if (Handlers.ContainsKey(key))
            {
                foreach (var item in Handlers[key])
                {
                    if (item is IHandle<TMessage> handler)
                    {
                        handler.Handle(message).FireAndForget(false);
                    }
                }
            }

            SignalToHandlers.Set();
        }

        public MessageHub()
        {
            Handlers = new Dictionary<Type, HashSet<object>>();
            SignalToHandlers = new ManualResetEvent(true);
        }

        private readonly Dictionary<Type, HashSet<object>> Handlers;
        private readonly ManualResetEvent SignalToHandlers;


        #region IDisposable Support

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) { SignalToHandlers.Dispose(); }
                Handlers.Clear();
                disposed = true;
            }
        }

        private bool disposed;

        #endregion
    }
}

