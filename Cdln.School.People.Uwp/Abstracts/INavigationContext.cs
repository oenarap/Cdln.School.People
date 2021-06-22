using System;

namespace Cdln.School.People.Uwp
{
    public interface INavigationContext
    {
        void Add<T>(T instance);
        void Remove<T>();
        T GetInstance<T>();
    }
}
