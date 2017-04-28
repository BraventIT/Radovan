using System;

namespace Radovan.Plugin.Core.Enums
{
    /// <summary>
    /// For swipe gestures determines the general
    /// direction of the swipe
    /// </summary>
    [Flags]
    public enum Directionality
    {
        /// <summary>
        /// No direction
        /// </summary>
        None = 0,
        /// <summary>
        /// Swiping from Right to Left
        /// </summary>
        Left = 0x01,
        /// <summary>
        /// Swiping from Left to Right
        /// </summary>
        Right = 0x02,
        /// <summary>
        /// Swiping from Bottom to Top
        /// </summary>
        Up = 0x10,
        /// <summary>
        /// Swiping from Top to Bottom
        /// </summary>
        Down = 0x20,
        /// <summary>
        /// Mask to isolate the Horizontal component
        /// </summary>
        HorizontalMask = 0x0F,
        /// <summary>
        /// Mask to isolate the Vertical component
        /// </summary>
        VerticalMask = 0xF0
    }
}
