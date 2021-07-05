using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.UI.Xaml.Input;

namespace School.People.Uwp.Controls
{
    /// <summary>
    /// A BladeView-style layout that adapts to the
    /// available size of its host.
    /// </summary>
    public sealed partial class AdaptiveBladeView : ItemsControl
    {
        private Grid rootGrid;
        private int minBladeLength = 0;
        private int currentIndex = 0;
        private readonly IList<Blade> ActiveBlades = new List<Blade>();


        private Blade this[int index]
        {
            get
            {
                if (Items[index] is Blade blade) { return blade; }
                return (Blade)ContainerFromIndex(index);
            }
        }

        private static void OnDesiredBladeLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            var desiredLength = (int)e.NewValue;

            view.minBladeLength = (int)(desiredLength * 0.625);
        }



        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PART_RootGrid") is Grid grid)
            {
                rootGrid = grid;
                rootGrid.SizeChanged += OnRootGridSizeChanged;
            }
        }

        // 1. RESIZE TRIGGERED
        private void OnRootGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = e.NewSize.Width;

            if (width == e.PreviousSize.Width || double.IsNaN(width)) { return; }

            var maxBlades = Math.DivRem((int)width, DesiredBladeLength, out int remainder);
            maxBlades += remainder / minBladeLength;

            SetValue(MaxBladeCountProperty, maxBlades); // step 2
            SetValue(ActualBladeLengthProperty, width / maxBlades); // step 3
        }

        // 2. SELECT BLADE(S) THAT CAN BE DISPLAYED
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
                        var nextBladeProminence = (int)GetProminence(nextBlade);

                        if (prominenceTotal + nextBladeProminence <= maxBladeCount)
                        {
                            nextBlade.Visibility = Visibility.Visible;
                            view.ActiveBlades.Add(nextBlade);
                            prominenceTotal += nextBladeProminence;
                        }
                        else
                        {
                            nextBlade.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        // STEP 3: ADJUST BLADES' WIDTHS
        private static void OnActualBladeLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            var length = (double)e.NewValue;
            var availableLength = length * view.MaxBladeCount;
            var lastIndex = view.ActiveBlades.Count - 1;

            for (int i = 0; i < lastIndex; i++)
            {
                var blade = view.ActiveBlades[i];
                var prominence = (int)GetProminence(blade);
                var bladeWidth = length * prominence;

                availableLength -= bladeWidth;
                blade.SetValue(WidthProperty, bladeWidth);
            }

            view.ActiveBlades[lastIndex].SetValue(WidthProperty, availableLength);
        }



        public AdaptiveBladeView()
        {
            this.DefaultStyleKey = typeof(AdaptiveBladeView);
        }

        /// <inheritdoc/>
        protected override DependencyObject GetContainerForItemOverride()
        {
            var container = new Blade();
            container.Tapped += SetCurrentIndex;
            container.GotFocus += SetCurrentIndex;

            container.SetBinding(HeightProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath("Height"),
                Mode = BindingMode.OneWay
            });

            return container;
        }

        private void SetCurrentIndex(object sender, RoutedEventArgs e)
        {
            if (sender is Blade blade)
            { currentIndex = Items.IndexOf(ItemFromContainer(blade)); }
        }

        /// <inheritdoc/>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is Blade;
        }

        /// <inheritdoc/>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (item is DependencyObject dObject)
            {
                SetProminence(element, GetProminence(dObject));
            }
            else
            {
                SetProminence(element, BladeProminence.Normal);
            }

            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
