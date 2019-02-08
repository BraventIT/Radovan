﻿using System;
namespace Radovan.Plugin.Core.Behaviors
{
    /// <summary>
    /// Syncronous Implmentation of the IGesture
    /// Paramater is a objet type
    /// </summary>
    public class RelayGesture : IGesture
    {
        private readonly Action<GestureResult, object> _execute;
        private readonly Func<GestureResult, object, bool> _canexecute;

        /// <summary>
        /// Builds the Rely Gesture
        /// </summary>
        /// <param name="execute">The action to execute when the gesture occures</param>
        /// <param name="predicate">A function to determine if the action should fire. If ommited the action is always available.</param>
        public RelayGesture(Action<GestureResult, object> execute, Func<GestureResult, object, bool> predicate = null)
        {
            this._execute = execute;
            _canexecute = predicate;
        }

        /// <summary>
        /// Excutes the action assoicated with the gesture
        /// </summary>
        /// <param name="result">The final <see cref="GestureResult"/></param>
        /// <param name="annoyingbaseobjectthing">The Gesture Paramater</param>
        public void ExecuteGesture(GestureResult result, object annoyingbaseobjectthing)
        {
            if (_execute != null) _execute(result, annoyingbaseobjectthing);
        }

        /// <summary>
        /// Tests to see if a gesture's action can execute
        /// </summary>
        /// <param name="result">The final <see cref="GestureResult"/></param>
        /// <param name="annoyingbaseobjectthing">The Gesture Paramater</param>
        /// <returns>true if the action can execute,false othewise</returns>
        public bool CanExecuteGesture(GestureResult result, object annoyingbaseobjectthing)
        {
            return (_canexecute == null || _canexecute(result, annoyingbaseobjectthing));
        }
    }
}
