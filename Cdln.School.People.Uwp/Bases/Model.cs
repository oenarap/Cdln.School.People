using System;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using System.Collections.Specialized;

namespace Cdln.School.People.Uwp
{
    /// <summary>
    /// Base model implementation. 
    /// Provides support for identifying a blank and/or modified model instance.
    /// </summary>
    /// <remarks>Support is limited to maximum of 24 fields only.</remarks>
    [DataContract]
    public abstract class Model : DependencyObject
    {
        /// <summary>
        /// Gets or sets the initialization flag.
        /// Any changes in values of participating fields will
        /// update IsModified property, after this property is set to false.
        /// </summary>
        protected bool IsInitializing { get; set; }

        public bool IsBlank
        {
            get { return Markers.Data > 2147483645; }  //max is 2147483645 with default bit discounted
        }

        public bool IsModified
        {
            get { return !Markers[Default]; }
        }

        protected Model(Guid id)
        {
            Id = id;
            IsInitializing = true;
        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        protected BitVector32 Markers = new BitVector32(Int32.MaxValue); // set all bits to [1] except 32nd

        // masks for 24 fields
        protected static readonly int Default = BitVector32.CreateMask();
        protected static readonly int M00 = BitVector32.CreateMask(Default);
        protected static readonly int M01 = BitVector32.CreateMask(M00);
        protected static readonly int M02 = BitVector32.CreateMask(M01);
        protected static readonly int M03 = BitVector32.CreateMask(M02);
        protected static readonly int M04 = BitVector32.CreateMask(M03);
        protected static readonly int M05 = BitVector32.CreateMask(M04);
        protected static readonly int M06 = BitVector32.CreateMask(M05);
        protected static readonly int M07 = BitVector32.CreateMask(M06);
        protected static readonly int M08 = BitVector32.CreateMask(M07);
        protected static readonly int M09 = BitVector32.CreateMask(M08);
        protected static readonly int M10 = BitVector32.CreateMask(M09);
        protected static readonly int M11 = BitVector32.CreateMask(M10);
        protected static readonly int M12 = BitVector32.CreateMask(M11);
        protected static readonly int M13 = BitVector32.CreateMask(M12);
        protected static readonly int M14 = BitVector32.CreateMask(M13);
        protected static readonly int M15 = BitVector32.CreateMask(M14);
        protected static readonly int M16 = BitVector32.CreateMask(M15);
        protected static readonly int M17 = BitVector32.CreateMask(M16);
        protected static readonly int M18 = BitVector32.CreateMask(M17);
        protected static readonly int M19 = BitVector32.CreateMask(M18);
        protected static readonly int M20 = BitVector32.CreateMask(M19);
        protected static readonly int M21 = BitVector32.CreateMask(M20);
        protected static readonly int M22 = BitVector32.CreateMask(M21);
        protected static readonly int M23 = BitVector32.CreateMask(M22);
    }
}
