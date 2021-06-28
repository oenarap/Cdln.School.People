using System;
using Windows.UI.Xaml;

namespace Cdln.School.People.Uwp
{
    /// <summary>
    /// Provides base implementation for models that
    /// participates in message routing/handling.
    /// </summary>
    public abstract class MessagingModel : DependencyObject, IDisposable
    {
        protected readonly IMessageHub Hub;
        protected readonly Guid Id;

        public MessagingModel(IMessageHub hub)
        {
            Hub = hub;
            Id = Guid.NewGuid();
            RegisterHandledMessages(hub);
        }

        /// <summary>
        /// Override this method to define the message handling registration logic.
        /// </summary>
        protected virtual void RegisterHandledMessages(IMessageHub hub) { }

        /// <summary>
        /// Override this method to everse the message handling registration logic.
        /// </summary>
        protected virtual void UnregisterHandledMessages(IMessageHub hub) { }


        #region IDisposable Support

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) { UnregisterHandledMessages(Hub); }
                disposed = true;
            }
        }

        private bool disposed;

        #endregion
    }
}
