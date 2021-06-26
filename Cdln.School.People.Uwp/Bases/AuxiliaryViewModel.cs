using Windows.UI.Xaml;
using School.People.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Cdln.School.People.Uwp.Messages;
using Cdln.School.People.Uwp.ViewModels;

namespace Cdln.School.People.Uwp
{
    public abstract class AuxiliaryViewModel : DependencyObject, IHandle<CurrentPersonChangedEvent>, IHandle<NoCurrentPersonEvent>
    {
        public async Task Handle(CurrentPersonChangedEvent message)
        {
            if (message.Data is IPerson p)
            {
                SaveChanges().FireAndForget(false);
                await RequestData(p);
            }
            else { await ResetData(); }
        }

        public async Task Handle(NoCurrentPersonEvent message)
        {
            await SaveChanges();
            await ResetData();
        }

        protected abstract Task RequestData(IPerson person);
        protected abstract Task SaveChanges();
        protected abstract Task ResetData();

        protected AuxiliaryViewModel(PeopleListViewModel peopleListViewModel)
        {
            if (peopleListViewModel.View?.CurrentItem is IPerson p) { RequestData(p); }
        }
    }
}
