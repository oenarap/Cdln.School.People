using System;
using Windows.UI.Xaml;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace School.People.Uwp.Controls
{
    [DefaultProperty(nameof(Content))]
    public class Blade : Control
    {
        /// <summary>
        /// Identifies the <see cref="ContentTemplate"/> property.
        /// </summary>
        private static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(Blade), new PropertyMetadata(null));

        /// <summary>
        /// Gets/sets the <see cref="Blade"/>'s content template.
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Content"/> property.
        /// </summary>
        private static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(Blade), new PropertyMetadata(null));

        /// <summary>
        /// Gets/sets the <see cref="Blade"/>'s content.
        /// </summary>
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public Blade() => DefaultStyleKey = typeof(Blade);
    }
}
