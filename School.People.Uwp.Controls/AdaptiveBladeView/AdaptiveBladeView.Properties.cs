using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace School.People.Uwp.Controls
{
    public partial class AdaptiveBladeView
    {
        /// <summary>
        /// Identifies the <see cref="ItemsSource"/> property.
        /// </summary>
        private static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(AdaptiveBladeView), new PropertyMetadata(null, OnItemsSourcePropertyChanged));

        /// <summary>
        /// Gets/sets the items source of the view.
        /// </summary>
        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Prominence"/> attached property.
        /// </summary>
        public static readonly DependencyProperty ProminenceProperty = DependencyProperty.RegisterAttached("Prominence", typeof(BladeProminence), typeof(AdaptiveBladeView), new PropertyMetadata(BladeProminence.Normal));

        /// <summary>
        /// Gets the Prominence attached property of an item.
        /// </summary>
        public static BladeProminence GetProminence(DependencyObject item)
        {
            return (BladeProminence)item?.GetValue(ProminenceProperty);
        }

        /// <summary>
        /// Sets the Prominence attached property value of an item.
        /// </summary>
        public static void SetProminence(DependencyObject item, BladeProminence value)
        {
            item?.SetValue(ProminenceProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="DesiredBladeLength"/> property.
        /// </summary>
        private static readonly DependencyProperty DesiredBladeLengthProperty = DependencyProperty.Register(nameof(DesiredBladeLength), typeof(int), typeof(AdaptiveBladeView), new PropertyMetadata(0, OnDesiredBladeLengthPropertyChanged));

        /// <summary>
        /// Gets/sets the desired blades' length.
        /// </summary>
        public int DesiredBladeLength
        {
            get { return (int)GetValue(DesiredBladeLengthProperty); }
            set { SetValue(DesiredBladeLengthProperty, Math.Abs(value)); }
        }

        /// <summary>
        /// Identifies the <see cref="MaxBladeCount"/> property.
        /// </summary>
        private static readonly DependencyProperty MaxBladeCountProperty = DependencyProperty.Register(nameof(MaxBladeCount), typeof(int), typeof(AdaptiveBladeView), new PropertyMetadata(0, OnMaxBladeCountPropertyChanged));

        /// <summary>
        /// Gets the current blade count.
        /// </summary>
        public int MaxBladeCount => (int)GetValue(MaxBladeCountProperty);

        /// <summary>
        /// Identifies the <see cref="Orientation"/> property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(AdaptiveBladeView), new PropertyMetadata(Orientation.Horizontal, OnOrientationPropertyChanged));

        /// <summary>
        /// Gets/sets the <see cref="AdaptiveBladeView"/>'s orientation.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
    }
}
