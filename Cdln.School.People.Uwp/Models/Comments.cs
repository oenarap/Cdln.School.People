using System;
using Windows.UI.Xaml;
using School.People.Core.Attributes;

namespace Cdln.School.People.Uwp.Models
{
    public sealed class Comments : Model, IComments
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(string), typeof(Comments), new PropertyMetadata(null, OnContentPropertyChanged));

        public Comments(Guid id, string content)
            : base(id)
        {
            SetValue(ContentProperty, content);
            IsInitializing = false;
        }

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Comments model = (Comments)d;
            string str = (string)e.NewValue;
            model.Markers[M00] = string.IsNullOrEmpty(str);
            model.Markers[Default] = model.IsInitializing;
        }
    }
}
