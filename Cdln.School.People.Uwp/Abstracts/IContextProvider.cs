using System;
using Windows.UI.Xaml.Data;

namespace Cdln.School.People.Uwp
{
    public interface IContextProvider
    {
        ICollectionView Contexts { get; }
    }
}
