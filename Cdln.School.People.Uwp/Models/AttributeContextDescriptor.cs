using System;
using Windows.UI.Xaml.Input;

namespace Cdln.School.People.Uwp.Models
{
    public class AttributeContextDescriptor : ContextDescriptor<string>
    {
        public AttributeContextDescriptor(string glyph, string description, Type associatedViewType)
            : this(glyph, description, null, associatedViewType) { }

        public AttributeContextDescriptor(string glyph, string description, KeyboardAccelerator keyboardAccelerator)
            : this(glyph, description, keyboardAccelerator, null) { }

        public AttributeContextDescriptor(string glyph, string description, KeyboardAccelerator keyboardAccelerator, Type associatedViewType)
            : base(glyph, description, keyboardAccelerator, associatedViewType) { }
    }
}
