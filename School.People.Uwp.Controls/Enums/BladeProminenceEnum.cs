using System;
using System.ComponentModel;
using Windows.Foundation.Metadata;

namespace School.People.Uwp.Controls
{
    /// <summary>
    /// Defines the prominence (length ratio) of a blade
    /// against the other blades in <see cref="AdaptiveBladeView"/>.
    /// </summary>
    public enum BladeProminence
    {
        /// <summary>
        /// Default value.
        /// The blade occupies a single column or row.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Allows the blade to span two columns or rows.
        /// </summary>
        Prominent = 2

        ///// <summary>
        ///// The blade will occupy the entire length of the view.
        ///// </summary>
        //Maximized = 3,

        ///// <summary>
        ///// The blade will not be displayed.
        ///// </summary>
        //Hidden = 0
    }
}
