using System;
using Windows.UI.Xaml.Input;

namespace Cdln.School.People.Uwp
{
    public interface IContextDescriptor
    {
        Type AssociatedViewType { get; }
        string Description { get; }
        string Glyph { get; }
        KeyboardAccelerator KeyboardAccelerator { get; set; }
    }
}