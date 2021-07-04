using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

namespace School.People.Uwp.Controls
{
    public partial class AdaptiveBladeView
    {
        private static readonly GridLength length = new GridLength(1, GridUnitType.Star);

        private void SetCurrentIndex(Blade blade) => currentIndex = GetIndexOf(blade);

        private static int CalculateVisibleBladeCount(double availableLength, int desiredBladeLength, int minBladeLength)
        {
            try
            {
                var maxBlades = Math.DivRem((int)availableLength, desiredBladeLength, out int remainder);
                return maxBlades + (remainder / minBladeLength);
            }
            catch 
            { 
                return 0; 
            }
        }

        private int GetIndexOf(Blade blade)
        {
            for (var i = 0; i < blades.Length; i++)
            {
                if (blades[i] == blade) { return i; }
            }
            return 0;
        }

        private void PrepareBladesForItems(int[] indexes)
        {
            rootGrid.Children.Clear();

            var pos = 0;

            foreach (var index in indexes)
            {
                var blade = this[index];
                var weight = weights[index];

                Grid.SetColumn(blade, pos);
                Grid.SetRow(blade, pos);
                Grid.SetColumnSpan(blade, weight);
                Grid.SetRowSpan(blade, weight);

                pos += weight;
                rootGrid.Children.Add(blade);
            }
        }

        private int[] GetDisplayableItemsIndices(int bladeCount, int currentIndex)
        {
            var weightTotal = weights[currentIndex];

            if (weightTotal >= bladeCount || items.Length == 1) { return new int[1] { currentIndex }; }

            var indexes = new List<int>() { currentIndex };
            var nextIndex = currentIndex;
            var prevIndex = currentIndex;

            for (var i = 0; i < bladeCount; i++)
            {
                prevIndex -= 1;

                if (prevIndex >= 0)
                {
                    indexes.Insert(0, prevIndex);
                    weightTotal += weights[prevIndex];
                }

                if (weightTotal >= bladeCount) { break; }

                nextIndex += 1;

                if (nextIndex < items.Length) 
                {
                    indexes.Add(nextIndex);
                    weightTotal += weights[nextIndex]; 
                }

                if (weightTotal >= bladeCount) { break; }
            }

            return indexes.ToArray();
        }

        private void UpdateColumns(int count)
        {
            var diff = count - rootGrid.ColumnDefinitions.Count;

            if (diff > 0)
            {
                var startingIndex = rootGrid.ColumnDefinitions.Count;

                for (int i = startingIndex; i < startingIndex + diff; i++)
                {
                    var coldef = new ColumnDefinition() { Width = length };
                    rootGrid.ColumnDefinitions.Add(coldef);
                }
            }
            else if (diff < 0)
            {
                var lastIndex = rootGrid.ColumnDefinitions.Count - 1;

                for (int i = lastIndex; i > lastIndex + diff; i--)
                {
                    rootGrid.ColumnDefinitions.RemoveAt(i);
                }
            }
        }

        private void UpdateRows(int count)
        {
            var diff = count - rootGrid.RowDefinitions.Count;

            if (diff > 0)
            {
                var startingIndex = rootGrid.RowDefinitions.Count;

                for (int i = startingIndex; i < startingIndex + diff; i++)
                {
                    var rowDef = new RowDefinition() { Height = length };
                    rootGrid.RowDefinitions.Add(rowDef);
                }
            }
            else if (diff < 0)
            {
                var lastIndex = rootGrid.RowDefinitions.Count - 1;

                for (int i = lastIndex; i > lastIndex + diff; i--)
                {
                    rootGrid.RowDefinitions.RemoveAt(i);
                }
            }
        }
    }
}
