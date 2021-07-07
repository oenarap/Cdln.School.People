using System;
using Windows.UI.Xaml;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace School.People.Uwp.Controls
{
    public class Blade : ContentControl
    {
        /// <summary>
        /// Identifies the <see cref="BackgroundOpacity"/> property.
        /// </summary>
        public static readonly DependencyProperty BackgroundOpacityProperty = DependencyProperty.Register(nameof(BackgroundOpacity), typeof(double), typeof(Blade), new PropertyMetadata(0.0));

        /// <summary>
        /// Gets/sets the opacity of blade's background.
        /// </summary>
        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        /// <summary>
        /// Expands the blade by doubling its current length.
        /// </summary>
        public void Expand()
        {

        }

        /// <summary>
        /// Resizes the blade to its default length.
        /// </summary>
        public void Restore()
        {

        }

        /// <summary>
        /// Hides the blade.
        /// </summary>
        public void Collapse()
        {

        }

        /// <inheritdoc/>
        public Blade() => DefaultStyleKey = typeof(Blade);
    }
}
