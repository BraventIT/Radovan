namespace Radovan.Plugin.Core.Enums
{
    /// <summary>
    /// The base supported gestures
    /// </summary>
    public enum GestureType
    {
        /// <summary>
        /// No Gesture
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Single tap
        /// </summary>
        SingleTap,
        /// <summary>
        /// Double Tap
        /// </summary>
        DoubleTap,
        /// <summary>
        /// LongPress
        /// </summary>
        LongPress,
        /// <summary>
        /// Swipe, Swipe is combined with Directionality to support:
        /// SwipeLeft
        /// SwipeRight
        /// SwipeUp
        /// SwipeDown
        /// It is very possible for a single swipe action to trigger two Swipe events:
        /// ie:  SwipeUp and SwipeLeft
        /// </summary>
        Swipe,
        /// <summary>
        /// 2 finger pinch.  Origin2 will contain the location of the second finger.
        /// </summary>
        Pinch,
        /// <summary>
        /// 1 finger move
        /// </summary>
        Move,
        /// <summary>
        /// All up events send this geture.  It can be ignored except for when you want to detect the end of a Pinch or Move.
        /// </summary>
        Up,
        /// <summary>
        /// All down events send this geture.  It can be ignored except for when you want to detect when the user start touching the screen.
        /// </summary>
        Down,
    }
}
