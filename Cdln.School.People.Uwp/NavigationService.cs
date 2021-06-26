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
        private readonly Frame frame;
        private NavigationContext context;

        public bool CanGoBack => frame.CanGoBack;

        public bool CanGoForward => frame.CanGoForward;

        public NavigationService(Frame initialFrame = null)
        {
            frame = initialFrame ?? Window.Current.Content as Frame;
            frame.NavigationFailed += OnNavigationFailed;
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Navigate(typeof(ErrorPage), context);
        }

        public bool GoBack()
        {
            if (CanGoBack)
            {
                int lastIndex = frame.BackStackDepth - 1;
                var entry = frame.BackStack[lastIndex];
                frame.BackStack.RemoveAt(lastIndex);
                var result = Navigate(entry.SourcePageType, context);
                if (result == true) { frame.BackStack.RemoveAt(lastIndex); }
                return result;
            }
            return false;
        }

        public void GoForward() => frame.GoForward();

        public bool Navigate(Type sourcePageType, NavigationContext context = null, NavigationTransitionInfo navigationTransitionInfo = null)
        {
            var result = frame.Navigate(sourcePageType, context, navigationTransitionInfo);
            if (result == true) { this.context = context; }
            return result;
        }

        public bool NavigateBack(Type sourcePageType, NavigationContext context = null, NavigationTransitionInfo navigationTransitionInfo = null)
        {
            int count = frame.BackStackDepth;
            for (int i = count - 1; i >= 0; i--)
            {
                var entry = frame.BackStack[i];
                frame.BackStack.RemoveAt(i);
                if (entry.SourcePageType == sourcePageType) { return Navigate(sourcePageType, context, navigationTransitionInfo); }
            }
            return false;
        }
    }
}
