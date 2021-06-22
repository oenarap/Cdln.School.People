using System;
using Windows.UI.Xaml.Input;

namespace Cdln.School.People.Uwp.Models
{
    public class PeopleContextDescriptor : ContextDescriptor<PeopleContext>
    {
        public PeopleContextDescriptor(string glyph, PeopleContext description, KeyboardAccelerator keyboardAccelerator)
            : this(glyph, description, keyboardAccelerator, null) { }

        public PeopleContextDescriptor(string glyph, PeopleContext description, KeyboardAccelerator keyboardAccelerator, Type associatedViewType)
            : base(glyph, description, keyboardAccelerator, associatedViewType) { }
    }
}
