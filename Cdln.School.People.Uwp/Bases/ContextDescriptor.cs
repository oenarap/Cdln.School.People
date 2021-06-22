using System;
using Windows.UI.Xaml.Input;

namespace Cdln.School.People.Uwp
{
    public abstract class ContextDescriptor<T>
    {
        protected ContextDescriptor(string glyph, T description, KeyboardAccelerator keyboardAccelerator, Type associatedViewType)
        {
            Glyph = glyph.Substring(0, 1);
            Description = description;
            KeyboardAccelerator = keyboardAccelerator;
            AssociatedViewType = associatedViewType;
        }

        public string Glyph { get; }
        public T Description { get; }
        public KeyboardAccelerator KeyboardAccelerator { get; }
        public Type AssociatedViewType { get; }
    }
}
