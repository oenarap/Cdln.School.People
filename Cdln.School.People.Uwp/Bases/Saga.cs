using System;

namespace Cdln.School.People.Uwp
{
    public abstract class Saga : IDisposable
    {
        protected readonly IMessageHub MessageHub;
        protected readonly Guid Id;
        protected bool disposed;

        public Saga(IMessageHub messageHub)
        {
            MessageHub = messageHub;
            RegisterHandledMessages(MessageHub);
            Id = Guid.NewGuid();
        }

        protected abstract void RegisterHandledMessages(IMessageHub messageHub);
        protected abstract void UnregisterHandledMessages(IMessageHub messageHub);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                UnregisterHandledMessages(MessageHub);
                disposed = true;
            }
        }
    }
}
