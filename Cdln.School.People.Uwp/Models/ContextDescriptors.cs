using System;
using Windows.UI.Xaml.Input;

namespace Cdln.School.People.Uwp.Models
{
    public class PeopleContextDescriptor : ContextDescriptor<PeopleContext>
    {
        public PeopleContextDescriptor(PeopleContext type, string description, Type associatedType = null, string glyph = null, KeyboardAccelerator keyboardAccelerator = null)
            : base(type, description, glyph, associatedType, keyboardAccelerator) { }
    }

    public class AttributeContextDescriptor : ContextDescriptor<AttributeContext>
    {
        public AttributeContextDescriptor(AttributeContext type, string description, Type associatedType = null, string glyph = null, KeyboardAccelerator keyboardAccelerator = null)
            : base(type, description, glyph, associatedType, keyboardAccelerator) { }
    }
}
