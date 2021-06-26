using System;
using Windows.UI.Xaml.Media.Animation;

namespace Cdln.School.People.Uwp
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        bool CanGoForward { get; }

        bool GoBack();
        void GoForward();
        bool Navigate(Type sourcePageType, NavigationContext context = null, NavigationTransitionInfo navigationTransitionInfo = null);
        bool NavigateBack(Type sourcePageType, NavigationContext context = null, NavigationTransitionInfo navigationTransitionInfo = null);
    }
}