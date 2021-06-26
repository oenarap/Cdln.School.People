using System;
using Windows.UI.Xaml.Data;

namespace Cdln.School.People.Uwp
{
    public interface IPeopleContextsListViewModel
    {
        ICollectionView View { get; }
    }
}
