using System;
using Windows.UI.Xaml;
using Windows.UI.Core;
using School.People.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp
{
    public abstract class AttributeViewModel<TView> : DependencyObject, IDisposable, IHandle<AttributeContextChangedEvent>,
        IHandle<CurrentPersonChangedEvent>, IHandle<NoCurrentPersonEvent> where TView : Page
    {
        private static readonly DependencyProperty IdProperty = DependencyProperty.Register(nameof(Id), typeof(Guid), typeof(AttributeViewModel<TView>), new PropertyMetadata(Guid.Empty, OnIdPropertyChanged));
        private static readonly DependencyProperty IsCurrentProperty = DependencyProperty.Register(nameof(IsCurrent), typeof(bool), typeof(AttributeViewModel<TView>), new PropertyMetadata(false, OnIsCurrentPropertyChanged));

        private static void OnIdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (AttributeViewModel<TView>)d;
            if (e.OldValue is Guid oldId && oldId != Guid.Empty) { viewModel.SaveChanges(oldId); }
            if (e.NewValue is Guid newId && newId != Guid.Empty) { viewModel.RequestData(newId); }
            else { viewModel.ResetValues(); }
        }

        private static void OnIsCurrentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (AttributeViewModel<TView>)d;
            if ((bool)e.OldValue) { viewModel.SetValue(IdProperty, Guid.Empty); }
            if ((bool)e.NewValue) 
            {
                if (viewModel.PeopleListProvider.People?.CurrentItem is IPerson person)
                { 
                    if (viewModel.Id != person.Id) { viewModel.SetValue(IdProperty, person.Id); }
                    else { viewModel.RequestData(person.Id); } // fail-safe
                }
            }
        }

        public async Task Handle(NoCurrentPersonEvent message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                if (this.IsCurrent) { SetValue(IdProperty, Guid.Empty); } });
        }

        public async Task Handle(CurrentPersonChangedEvent message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (this.IsCurrent)
                {
                    if (message.Data is IPerson person) { SetValue(IdProperty, person.Id); }
                    else { SetValue(IdProperty, Guid.Empty); }
                }
            });
        }

        public async Task Handle(AttributeContextChangedEvent message)
        {
            var oldContextTarget = message.Data.OldValue?.AssociatedViewType;
            var newContextTarget = message.Data.NewValue?.AssociatedViewType;

            // CHECK ASSOCIATED TYPES - attribute context may have changed, but
            // the associated view (target) type may still be the same.
            if (oldContextTarget != newContextTarget)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (oldContextTarget is Type oldType && oldType == typeof(TView))
                    { SetValue(IsCurrentProperty, false); }
                    if (newContextTarget is Type newType && newType == typeof(TView))
                    { SetValue(IsCurrentProperty, true); }
                });
            }
        }

        protected virtual void RegisterHandledMessages(IMessageHub hub)
        {
            hub.RegisterHandler<AttributeViewModel<TView>, AttributeContextChangedEvent>(this);
            hub.RegisterHandler<AttributeViewModel<TView>, CurrentPersonChangedEvent>(this);
            hub.RegisterHandler<AttributeViewModel<TView>, NoCurrentPersonEvent>(this);
        }

        protected virtual void UnregisterHandledMessages(IMessageHub hub)
        {
            hub.UnregisterHandler<AttributeViewModel<TView>, AttributeContextChangedEvent>(this);
            hub.UnregisterHandler<AttributeViewModel<TView>, CurrentPersonChangedEvent>(this);
            hub.UnregisterHandler<AttributeViewModel<TView>, NoCurrentPersonEvent>(this);
        }


        /// <summary>
        /// Override this method to define data request behavior of the derived class.
        /// Acquire the requested data by registering to handle the message that bears the result.
        /// </summary>
        protected abstract void RequestData(Guid id);

        /// <summary>
        /// Defines a task to save changes made to the view's data.
        /// </summary>
        protected abstract void SaveChanges(Guid id);

        /// <summary>
        /// Override this method to set values for the view when no <see cref="IPerson"/> is selected.
        /// </summary>
        protected abstract void ResetValues();

        public AttributeViewModel(IMessageHub hub, PeopleListProvider people, Type currentType = null)
        {
            Hub = hub;
            PeopleListProvider = people;
            RegisterHandledMessages(Hub);

            SetValue(IsCurrentProperty, currentType == typeof(TView));
        }

        protected bool IsCurrent => (bool)GetValue(IsCurrentProperty);
        protected Guid Id => (Guid)GetValue(IdProperty);

        protected readonly PeopleListProvider PeopleListProvider;
        protected readonly IMessageHub Hub;


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
