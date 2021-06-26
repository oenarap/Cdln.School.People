using System;
using System.Linq;
using School.People.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public partial class PeopleListViewModel : IHandle<PeopleContextChangedEvent>, IHandle<PersonUpdatedEvent>, 
        IHandle<StudentArchivedEvent>
    {
        public async Task Handle(StudentArchivedEvent e)
        {
            if (PeopleContextsListViewModel.View?.CurrentItem is PeopleContextDescriptor context)
            {
                if (context.Description == PeopleContext.Students && e.Data is IPerson p)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        People.Remove(p);
                        View.MoveCurrentToPosition(0);
                    });
                }
            }
        }

        public async Task Handle(PersonnelArchivedEvent message)
        {
            if (PeopleContextsListViewModel.View?.CurrentItem is PeopleContextDescriptor context)
            {
                if (context.Description == PeopleContext.Personnel && message.Data is IPerson p)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        People.Remove(p);
                        View.MoveCurrentToPosition(0);
                    });
                }
            }
        }

        public async Task Handle(StudentInsertedEvent message)
        {
            if (PeopleContextsListViewModel.View?.CurrentItem is PeopleContextDescriptor context)
            {
                if (context.Description == PeopleContext.Students && message.Data is IPerson p)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        People.Insert(0, new Person(message.Id, p.LastName, p.FirstName, p.MiddleName, p.NameExtension, p.Title));
                        View.MoveCurrentToPosition(0);
                    });
                }
            }
        }

        public async Task Handle(PersonnelInsertedEvent message)
        {
            if (PeopleContextsListViewModel.View?.CurrentItem is PeopleContextDescriptor context)
            {
                if (context.Description == PeopleContext.Personnel && message.Data is IPerson p)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        People.Insert(0, new Person(message.Id, p.LastName, p.FirstName, p.MiddleName, p.NameExtension, p.Title));
                        View.MoveCurrentToPosition(0);
                    });
                }
            }
        }

        public async Task Handle(PeopleContextChangedEvent message)
        {
            if (message.Data is PeopleContextDescriptor context)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => SetViewModel(context));
            }
        }

        public async Task Handle(PersonUpdatedEvent message)
        {
            if (message.Data is IPerson person)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    var px = (from p in People where p.Id == person.Id select p).FirstOrDefault();
                    if (px != null)
                    {
                        int index = People.IndexOf(px);
                        People.RemoveAt(index);
                        People.Insert(index, person);
                    }
                });
            }
        }
    }
}
