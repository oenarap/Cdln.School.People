using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Cdln.School.People.Uwp.Views;
using Windows.UI.Xaml.Media.Animation;

namespace Cdln.School.People.Uwp
{
    public sealed class NavigationService : INavigationService
    {
        private Frame _Frame;

        private Frame Frame
        {
            get
            {
                if (_Frame != null) { return _Frame; }
                if (_Frame == null)
                {
                    _Frame = Window.Current.Content as Frame;

                    if (_Frame != null)
                    {
                        _Frame.NavigationFailed += OnNavigationFailed;
                    }
                }
                return _Frame;
            }
            set
            {
                _Frame = value;

                if (_Frame != null)
                {
                    _Frame.NavigationFailed += OnNavigationFailed;
                }
            }
        }
        private NavigationContext context;

        public bool CanGoBack => Frame.CanGoBack;

        public bool CanGoForward => Frame.CanGoForward;

        public NavigationService(Frame initialFrame = null)
        {
            Frame = initialFrame;
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Navigate(typeof(ErrorPage), context);
        }

        public bool GoBack()
        {
            if (CanGoBack)
            {
                int lastIndex = Frame.BackStackDepth - 1;
                var entry = Frame.BackStack[lastIndex];
                Frame.BackStack.RemoveAt(lastIndex);
                var result = Navigate(entry.SourcePageType, context);
                if (result == true) { Frame.BackStack.RemoveAt(lastIndex); }
                return result;
            }
            return false;
        }

        public void GoForward() => Frame.GoForward();

        public bool Navigate(Type sourcePageType, NavigationContext context = null, NavigationTransitionInfo navigationTransitionInfo = null)
        {
            var result = Frame.Navigate(sourcePageType, context, navigationTransitionInfo);
            if (result == true) { this.context = context; }
            return result;
        }

        public bool NavigateBack(Type sourcePageType, NavigationContext context = null, NavigationTransitionInfo navigationTransitionInfo = null)
        {
            int count = Frame.BackStackDepth;
            for (int i = count - 1; i >= 0; i--)
            {
                var entry = Frame.BackStack[i];
                Frame.BackStack.RemoveAt(i);
                if (entry.SourcePageType == sourcePageType) { return Navigate(sourcePageType, context, navigationTransitionInfo); }
            }
            return false;
        }
    }
}
