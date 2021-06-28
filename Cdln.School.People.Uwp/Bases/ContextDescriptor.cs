using System;
using Windows.UI.Xaml.Input;

namespace Cdln.School.People.Uwp
{
    public abstract class ContextDescriptor<T> : IContextDescriptor
    {
        protected ContextDescriptor(T type, string description, string glyph = null, Type associatedViewType = null,
            KeyboardAccelerator keyboardAccelerator = null)
        {
            Type = type;
            Description = description;
            AssociatedViewType = associatedViewType;
            Glyph = glyph?.Substring(0, 1);
            KeyboardAccelerator = keyboardAccelerator;
        }

        public T Type { get; }
        public string Description { get; }
        public string Glyph { get; }
        public KeyboardAccelerator KeyboardAccelerator { get; set; }
        public Type AssociatedViewType { get; }
    }
}
