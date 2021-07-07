using System;
using System.Collections.Generic;

namespace Cdln.School.People.Uwp
{
    /// <summary>
    /// This class is devised to keep track of contexts set by
    /// various pages as the user navigates through them.
    /// </summary>
    public class NavigationContext : INavigationContext
    {
        private readonly Dictionary<Type, object> NavigationParameters;
        public NavigationService Root { get; }

        public NavigationContext(NavigationService root = null)
        {
            Root = root;
            NavigationParameters = new Dictionary<Type, object>();
        }

        public void Add<T>(T instance)
        {
            var key = typeof(T);
            if (NavigationParameters.ContainsKey(key)) { return; }
            NavigationParameters.Add(key, instance);
        }

        public void Remove<T>()
        {
            var key = typeof(T);
            if (NavigationParameters.ContainsKey(key)) { NavigationParameters.Remove(key); }
        }

        public T GetInstance<T>()
        {
            var key = typeof(T);
            if (NavigationParameters.ContainsKey(key)) { return (T)NavigationParameters[key]; }
            return default;
        }
    }
}
