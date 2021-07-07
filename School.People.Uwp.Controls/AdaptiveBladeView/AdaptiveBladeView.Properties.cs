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
        /// Identifies the <see cref="DesiredBladeLength"/> property.
        /// </summary>
        private static readonly DependencyProperty DesiredBladeLengthProperty = DependencyProperty.Register(nameof(DesiredBladeLength), typeof(int), typeof(AdaptiveBladeView), new PropertyMetadata(0, OnDesiredBladeLengthPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="ActualBladeLength"/> property.
        /// </summary>
        private static readonly DependencyProperty ActualBladeLengthProperty = DependencyProperty.Register(nameof(ActualBladeLength), typeof(double), typeof(AdaptiveBladeView), new PropertyMetadata(0, OnActualBladeLengthPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="MaxBladeCount"/> property.
        /// </summary>
        private static readonly DependencyProperty MaxBladeCountProperty = DependencyProperty.Register(nameof(MaxBladeCount), typeof(int), typeof(AdaptiveBladeView), new PropertyMetadata(0, OnMaxBladeCountPropertyChanged));



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
        /// Gets/sets the desired blades' length.
        /// </summary>
        public int DesiredBladeLength
        {
            get { return (int)GetValue(DesiredBladeLengthProperty); }
            set { SetValue(DesiredBladeLengthProperty, Math.Abs(value)); }
        }

        /// <summary>
        /// Gets the actual blades' length.
        /// </summary>
        public double ActualBladeLength => (double)GetValue(ActualBladeLengthProperty);

        
        /// <summary>
        /// Gets the current blade count.
        /// </summary>
        public int MaxBladeCount => (int)GetValue(MaxBladeCountProperty);



        private static void OnDesiredBladeLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            var desiredLength = (int)e.NewValue;

            view.minBladeLength = (int)(desiredLength * 0.625);
        }

        private static void OnMaxBladeCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;

            if (view[view.currentIndex] is Blade currentBlade)
            {
                var maxBladeCount = (int)e.NewValue;
                var nextIndex = view.currentIndex;
                var prevIndex = view.currentIndex;

                view.ActiveBlades.Clear();
                view.ActiveBlades.Add(currentBlade);

                var prominenceTotal = (int)GetProminence(currentBlade);

                for (var i = 0; i < view.Items.Count; i++)
                {
                    prevIndex -= 1;

                    if (prevIndex >= 0 && view[prevIndex] is Blade previousBlade)
                    {
                        var prevBladeProminence = (int)GetProminence(previousBlade);

                        if (prominenceTotal + prevBladeProminence <= maxBladeCount)
                        {
                            previousBlade.Visibility = Visibility.Visible;
                            view.ActiveBlades.Insert(0, previousBlade);
                            prominenceTotal += prevBladeProminence;
                        }
                        else
                        {
                            previousBlade.Visibility = Visibility.Collapsed;
                        }
                    }

                    nextIndex += 1;

                    if (nextIndex < view.Items.Count && view[nextIndex] is Blade nextBlade)
                    {
                        if (prominenceTotal < maxBladeCount)
                        {
                            nextBlade.Visibility = Visibility.Visible;
                            view.ActiveBlades.Add(nextBlade);
                            prominenceTotal += (int)GetProminence(nextBlade);
                        }
                        else
                        {
                            nextBlade.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private static void OnActualBladeLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            var length = (double)e.NewValue;
            var availableLength = length * view.MaxBladeCount;
            var lastIndex = view.ActiveBlades.Count - 1;
            var opacityFactor = 1.0 / lastIndex;

            for (int i = 0; i < lastIndex; i++)
            {
                var blade = view.ActiveBlades[i];
                var prominence = (int)GetProminence(blade);
                var bladeWidth = length * prominence;

                blade.BackgroundOpacity = opacityFactor * i;
                availableLength -= bladeWidth;
                blade.SetValue(WidthProperty, bladeWidth);
            }

            view.ActiveBlades[lastIndex].BackgroundOpacity = 1.0;
            view.ActiveBlades[lastIndex].SetValue(WidthProperty, availableLength);
        }
    }
}
