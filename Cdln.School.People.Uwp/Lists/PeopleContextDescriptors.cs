using Windows.System;
using Windows.UI.Xaml.Input;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Views;
using Cdln.School.People.Uwp.Models;

namespace Cdln.School.People.Uwp.Lists
{
    public class PeopleContextDescriptors
    {
        public static IEnumerable<PeopleContextDescriptor> Items { get; } = new List<PeopleContextDescriptor>()
        {
            new PeopleContextDescriptor("", PeopleContext.Personnel, new KeyboardAccelerator() { Key = VirtualKey.F9 }, typeof(ActivePeopleView)),
            new PeopleContextDescriptor("", PeopleContext.Students, new KeyboardAccelerator() { Key = VirtualKey.F10 }, typeof(ActivePeopleView)),
            new PeopleContextDescriptor("", PeopleContext.Others, new KeyboardAccelerator() { Key = VirtualKey.F11 }, typeof(InactivePeopleView)),
            new PeopleContextDescriptor("", PeopleContext.Archived, new KeyboardAccelerator() { Key = VirtualKey.F12 }, typeof(InactivePeopleView))
        };
    }
}
