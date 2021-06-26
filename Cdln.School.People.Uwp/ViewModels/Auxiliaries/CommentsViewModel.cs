using System;
using Windows.UI.Xaml;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Messages;
using Cdln.School.People.Uwp.Views.Auxiliaries;

namespace Cdln.School.People.Uwp.ViewModels.Auxiliaries
{
    public class CommentsViewModel : AttributeViewModel<CommentsView>
    {
        private static readonly DependencyProperty CommentsProperty = DependencyProperty.Register(nameof(Comments), typeof(Comments), typeof(CommentsViewModel), new PropertyMetadata(null));

        public Comments Comments => (Comments)GetValue(CommentsProperty);


        protected override void RequestData(Guid id) => Hub.Dispatch(new CommentsQuery(Id, id));

        protected override void SaveChanges(Guid id)
        {
            if (Comments is Comments comments && comments.IsModified)
            {
                Hub.Dispatch(new SaveCommentsCommand(id, comments));
            }
        }

        protected override void ResetValues() => SetValue(CommentsProperty, default);

        public CommentsViewModel(IMessageHub hub, PeopleListViewModel people)
            : base(hub, people, typeof(CommentsView)) { }
    }
}
