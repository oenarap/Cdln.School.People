using System;
using Autofac;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.ViewModels;
using Windows.UI.Xaml.Data;

namespace Cdln.School.People.Uwp.Views.Panes
{
    public sealed partial class Conspectus : Page
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ConspectusViewModel), typeof(Conspectus), new PropertyMetadata(null));
        private static readonly DependencyProperty ContextProviderProperty = DependencyProperty.Register(nameof(ContextProvider), typeof(AttributeContextsProvider), typeof(Conspectus), new PropertyMetadata(null));
        private static readonly DependencyProperty PeopleListProviderProperty = DependencyProperty.Register(nameof(PeopleListProvider), typeof(PeopleListProvider), typeof(Conspectus), new PropertyMetadata(null));

        public ConspectusViewModel ViewModel => (ConspectusViewModel)GetValue(ViewModelProperty);
        public AttributeContextsProvider ContextProvider => (AttributeContextsProvider)GetValue(ContextProviderProperty);
        public PeopleListProvider PeopleListProvider => (PeopleListProvider)GetValue(PeopleListProviderProperty);

        public Command MoveNext
        {
            get
            {
                if (moveNext != null) { return moveNext; }
                if (moveNext == null)
                {
                    moveNext = new Command((param) =>
                    {
                        if (param is ICollectionView view)
                        {
                            var index = view.CurrentPosition + 1;
                            view.MoveCurrentToPosition(index);
                        }
                    }, (param) =>
                    {
                        if (param is ICollectionView view && view.Count > 0)
                        {
                            var index = view.CurrentPosition + 1;
                            return index < view.Count;
                        }
                        return false;
                    }, PeopleListProvider.People, null);
                }
                return moveNext;
            }
        }

        public Command MovePrevious
        {
            get
            {
                if (movePrevious != null) { return movePrevious; }
                if (movePrevious == null)
                {
                    movePrevious = new Command((param) =>
                    {
                        if (param is ICollectionView view)
                        {
                            var index = view.CurrentPosition - 1;
                            view.MoveCurrentToPosition(index);
                        }
                    }, (param) =>
                    {
                        if (param is ICollectionView view && view.Count > 0)
                        {
                            var index = view.CurrentPosition - 1;
                            return index >= 0;
                        }
                        return false;
                    }, PeopleListProvider.People, null);
                }
                return movePrevious;
            }
        }

        public Conspectus()
        {
            this.InitializeComponent();

            SetValue(PeopleListProviderProperty, App.Container.Resolve<PeopleListProvider>() ?? throw new NullReferenceException());
            SetValue(ContextProviderProperty, App.Container.Resolve<AttributeContextsProvider>() ?? throw new NullReferenceException());
            SetValue(ViewModelProperty, App.Container.Resolve<AttributePaneViewModel>() ?? throw new NullReferenceException());
        }

        private Command movePrevious;
        private Command moveNext;
    }
}
