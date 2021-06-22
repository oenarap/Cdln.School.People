using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Cdln.School.People.Uwp
{
    public sealed class NavigationService
    {
        private readonly Frame frame;
        private object lastparam;

        public bool CanGoBack => frame.CanGoBack;

        public bool CanGoForward => frame.CanGoForward;

        public NavigationService(Frame initialFrame = null)
        {
            frame = initialFrame ?? Window.Current.Content as Frame;
        }

        public bool GoBack()
        {
            if (CanGoBack)
            {
                int lastIndex = frame.BackStackDepth - 1;
                var entry = frame.BackStack[lastIndex];
                frame.BackStack.RemoveAt(lastIndex);
                var result = Navigate(entry.SourcePageType, lastparam);
                if (result == true) { frame.BackStack.RemoveAt(lastIndex); }
                return result;
            }
            return false;
        }

        public void GoForward() => frame.GoForward();

        public bool Navigate(Type sourcePageType, object parameter = null, NavigationTransitionInfo navigationTransitionInfo = null)
        {
            var result = frame.Navigate(sourcePageType, parameter, navigationTransitionInfo);
            if (result == true) { lastparam = parameter; }
            return result;
        }

        public bool NavigateBack(Type sourcePageType, object parameter = null, NavigationTransitionInfo navigationTransitionInfo = null)
        {
            int count = frame.BackStackDepth;
            for (int i = count - 1; i >= 0; i--)
            {
                var entry = frame.BackStack[i];
                frame.BackStack.RemoveAt(i);
                if (entry.SourcePageType == sourcePageType) { return Navigate(sourcePageType, parameter, navigationTransitionInfo); }
            }
            return false;
        }
    }
}
