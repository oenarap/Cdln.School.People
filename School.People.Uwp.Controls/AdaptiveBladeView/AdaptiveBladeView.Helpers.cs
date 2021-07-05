using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

namespace School.People.Uwp.Controls
{
    public partial class AdaptiveBladeView
    {
        private int maxActiveBladesCount;

        private void SelectActiveBlades(int maxBladeCount)
        {
            if (this[currentIndex] is Blade currentBlade)
            {
                var nextIndex = currentIndex;
                var prevIndex = currentIndex;

                ActiveBlades.Clear();
                ActiveBlades.Add(currentBlade);

                var prominenceTotal = (int)GetProminence(currentBlade);

                for (var i = 0; i < Items.Count; i++)
                {
                    prevIndex -= 1;

                    if (prevIndex >= 0 && this[prevIndex] is Blade previousBlade)
                    {
                        if (prominenceTotal < maxBladeCount)
                        {
                            previousBlade.Visibility = Visibility.Visible;
                            ActiveBlades.Insert(0, previousBlade);
                            prominenceTotal += (int)GetProminence(previousBlade);
                        }
                        else
                        {
                            previousBlade.Visibility = Visibility.Collapsed;
                        }
                    }

                    nextIndex += 1;

                    if (nextIndex < Items.Count && this[nextIndex] is Blade nextBlade)
                    {
                        if (prominenceTotal < maxBladeCount)
                        {
                            nextBlade.Visibility = Visibility.Visible;
                            ActiveBlades.Add(nextBlade);
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
    }
}
