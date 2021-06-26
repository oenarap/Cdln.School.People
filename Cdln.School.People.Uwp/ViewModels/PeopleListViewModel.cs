using System;
using System.Linq;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Data;
using Cdln.School.People.Uwp.Models;
using System.Collections.ObjectModel;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public partial class PeopleListViewModel : DependencyObject
    {
        private static readonly DependencyProperty ViewProperty = DependencyProperty.Register(nameof(View), typeof(ICollectionView), typeof(PeopleListViewModel), new PropertyMetadata(null, OnViewPropertyChanged));
        private static readonly DependencyProperty FilterKeyProperty = DependencyProperty.Register(nameof(FilterKey), typeof(string), typeof(PeopleListViewModel), new PropertyMetadata(null, OnFilterKeyPropertyChanged));

        private static void OnViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PeopleListViewModel model = (PeopleListViewModel)d;
            if (e.OldValue is ICollectionView oldView) { oldView.CurrentChanged -= model.OnViewCurrentChanged; }
            if (e.NewValue is ICollectionView view)
            {
                view.CurrentChanged += model.OnViewCurrentChanged;
                int index = Math.Min(view.Count, model.Logger.GetLog(model.Logkey));
                if (view.MoveCurrentToPosition(index)) { return; }
            }
            model.MessageHub.Dispatch(new NoCurrentPersonEvent(model.Id));
        }

        private void OnViewCurrentChanged(object sender, object e)
        {
            ICollectionView view = (ICollectionView)sender;
            if (view.CurrentItem is IPerson currentPerson)
            {
                Logger.Log(Logkey, view.CurrentPosition);
                MessageHub.Dispatch(new CurrentPersonChangedEvent(Id, currentPerson));
            }
            else { MessageHub.Dispatch(new NoCurrentPersonEvent(Id)); }
        }

        private void SetViewModel(PeopleContextDescriptor context)
        {
            Logkey = context.Description.ToString();
            Logger.RegisterKey(Logkey);
            SetValue(ViewProperty, null);
            Client.Get(this.Id, new Uri("https://localhost:44397/api/students/"));
        }

        private static void OnFilterKeyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PeopleListViewModel model = (PeopleListViewModel)d;
            string filterKey = (string)e.NewValue;

            if (string.IsNullOrEmpty(filterKey) || string.IsNullOrWhiteSpace(filterKey))
            {
                IPerson current = (IPerson)model.View.CurrentItem;
                CollectionViewSource viewSource = new CollectionViewSource() { Source = model.People };
                model.SetValue(ViewProperty, viewSource.View);
                if (current != null) { model.View.MoveCurrentTo(current); }
            }
            else
            {
                var filtered = (from p in model.People
                                where (p.LastName.Contains(filterKey, StringComparison.OrdinalIgnoreCase) ||
                                p.FirstName.Contains(filterKey, StringComparison.OrdinalIgnoreCase) ||
                                p.MiddleName.Contains(filterKey, StringComparison.OrdinalIgnoreCase))
                                select p).AsEnumerable();
                CollectionViewSource viewSource = new CollectionViewSource() { Source = filtered };
                model.SetValue(ViewProperty, viewSource.View);
                if (filtered.Count() > 0) { model.View.MoveCurrentToPosition(0); }
            }
        }

        public string FilterKey
        {
            get { return (string)GetValue(FilterKeyProperty); }
            set { SetValue(FilterKeyProperty, value); }
        }

        public ICollectionView View => (ICollectionView)GetValue(ViewProperty);

        public PeopleListViewModel(IApiClient client, IMessageHub messageHub, IIndexLogger logger, IPeopleContextsListViewModel peopleContextsListViewModel)
        {
            MessageHub = messageHub ?? throw new ArgumentNullException(nameof(messageHub));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Client = client ?? throw new ArgumentNullException(nameof(client));
            PeopleContextsListViewModel = peopleContextsListViewModel ?? throw new ArgumentNullException(nameof(peopleContextsListViewModel));
            //MessageHub.RegisterHandler<PeopleListViewModel, PeopleContextChangedEvent>();
            //MessageHub.RegisterHandler<PeopleListViewModel, PersonUpdatedEvent>();
            //MessageHub.RegisterHandler<PeopleListViewModel, PersonnelInsertedEvent>();
            //MessageHub.RegisterHandler<PeopleListViewModel, StudentInsertedEvent>();
            //MessageHub.RegisterHandler<PeopleListViewModel, PersonnelArchivedEvent>();
            //MessageHub.RegisterHandler<PeopleListViewModel, StudentArchivedEvent>();
            if (peopleContextsListViewModel.View.CurrentItem is PeopleContextDescriptor context) { SetViewModel(context); }
        }

        private readonly IPeopleContextsListViewModel PeopleContextsListViewModel;
        private ObservableCollection<IPerson> People;
        private readonly IIndexLogger Logger;
        private readonly IMessageHub MessageHub;
        private readonly IApiClient Client;
        private string Logkey;

        private readonly Guid Id = Guid.NewGuid();
    }
}
