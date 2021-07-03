using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

namespace School.People.Uwp.Controls
{
    /// <summary>
    /// A BladeView-style layout that adapts to the
    /// available size of its host.
    /// </summary>
    public sealed partial class AdaptiveBladeView : Control
    {
        private Grid rootGrid;
        private int minBladeLength = 0;
        private object[] items;
        private int[] weights;
        private Blade[] blades;
        private int currentIndex = 0;

        private Blade this[int index]
        {
            get
            {
                if (blades[index] != null) { return blades[index]; }
                
                blades[index] = new Blade() { Content = items[index] };
                blades[index].IsTabStop = true;
                blades[index].GotFocus += (sender, e) => SetCurrentIndex(sender as Blade);
                blades[index].Tapped += (sender, e) => SetCurrentIndex(sender as Blade);

                return blades[index];
            }
        }

        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            
            if (e.NewValue is IEnumerable<object> objects)
            {
                var items = new List<object>();
                var weights = new List<int>();

                foreach (var obj in objects)
                {
                    var item = obj as DependencyObject;
                    var weight = item != null ? (int)GetProminence(item) : 1;

                    items.Add(item);
                    weights.Add(weight);
                }

                view.items = items.ToArray();
                view.weights = weights.ToArray();
                view.blades = new Blade[items.Count];
                return;
            }

            view.items = new object[1] { e.NewValue };
            view.weights = new int[1] { 1 };
            view.blades = new Blade[1];
        }

        private static void OnOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            var isHorizontal = (Orientation)e.NewValue == Orientation.Horizontal;

            if (isHorizontal) { view.rootGrid.RowDefinitions.Clear(); }
            else { view.rootGrid.ColumnDefinitions.Clear(); }

            view.SetVisibleBladeCount(isHorizontal ? view.rootGrid.ActualWidth : view.rootGrid.ActualHeight);
        }

        private static void OnDesiredBladeLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            var desiredLength = (int)e.NewValue;

            view.minBladeLength = (int)(desiredLength * 0.625);
        }

        private static void OnMaxBladeCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (AdaptiveBladeView)d;
            var maxBladeCount = (int)e.NewValue;
            var diff = maxBladeCount - (int)e.OldValue;
            var isHorizontal = view.Orientation == Orientation.Horizontal;

            if (diff < 0) { _ = isHorizontal ? view.RemoveColumns(Math.Abs(diff)) : view.RemoveRows(Math.Abs(diff)); }
            else { _ = isHorizontal ? view.AppendColumns(diff) : view.AppendRows(diff); }

            var items = view.GetDisplayableItemsIndices(maxBladeCount, view.currentIndex);
            view.PrepareBladesForItems(items);
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

        private void OnRootGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var isHorizontal = Orientation == Orientation.Horizontal;
            var length = isHorizontal ? e.NewSize.Width : e.NewSize.Height;

            SetVisibleBladeCount(length);
        }

        private void SetVisibleBladeCount(double length)
        {
            if (double.IsNaN(length)) { return; }

            var maxBlades = Math.DivRem((int)length, DesiredBladeLength, out int remainder);

            SetValue(MaxBladeCountProperty, maxBlades + (remainder / minBladeLength));
        }

        public AdaptiveBladeView() => this.DefaultStyleKey = typeof(AdaptiveBladeView);
    }
}
