using Windows.UI.Xaml;
using Cdln.School.People.Uwp;
using Cdln.School.People.Uwp.Views.Auxiliaries;

namespace Cdln.School.Content.Uwp
{
    public class AuxiliaryViewModel : DependencyObject, IContentHost
    {
        private static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(AuxiliaryViewModel), new PropertyMetadata(null));

        public object Content => GetValue(ContentProperty);

        public AuxiliaryViewModel()
        {
            SetValue(ContentProperty, new CommentsView());
        }
    }
}