using System;
using Windows.UI.Xaml;

namespace School.People.Uwp.Controls
{
    /// <summary>
    /// A BladeView-style layout that adapts to the
    /// available size of its host.
    /// </summary>
    public partial class AdaptiveBladeView
    {
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
        /// Identifies the <see cref="ActualBladeLength"/> property.
        /// </summary>
        private static readonly DependencyProperty ActualBladeLengthProperty = DependencyProperty.Register(nameof(ActualBladeLength), typeof(double), typeof(AdaptiveBladeView), new PropertyMetadata(0, OnActualBladeLengthPropertyChanged));

        /// <summary>
        /// Gets/sets the actual blades' length.
        /// </summary>
        public double ActualBladeLength => (double)GetValue(ActualBladeLengthProperty);

        /// <summary>
        /// Identifies the <see cref="MaxBladeCount"/> property.
        /// </summary>
        private static readonly DependencyProperty MaxBladeCountProperty = DependencyProperty.Register(nameof(MaxBladeCount), typeof(int), typeof(AdaptiveBladeView), new PropertyMetadata(0, OnMaxBladeCountPropertyChanged));

        /// <summary>
        /// Gets the current blade count.
        /// </summary>
        public int MaxBladeCount => (int)GetValue(MaxBladeCountProperty);
    }
}
