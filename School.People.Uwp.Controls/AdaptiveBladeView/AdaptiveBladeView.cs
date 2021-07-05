using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

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
        private readonly IList<Blade> ActiveBlades;

        private Blade this[int index]
        {
            get
            {
                if (Items[index] is Blade blade) { return blade; }
                return (Blade)ContainerFromIndex(index);
            }
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

        public AdaptiveBladeView()
        {
            this.DefaultStyleKey = typeof(AdaptiveBladeView);
            this.ActiveBlades = new List<Blade>();
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
