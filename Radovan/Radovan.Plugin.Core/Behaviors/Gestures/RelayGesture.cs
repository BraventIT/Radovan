using System;
namespace Radovan.Plugin.Core.Behaviors
{
	public class RelayGesture : IGesture
	{
		private readonly Action<GestureResult, object> _execute;
		private readonly Func<GestureResult, object, bool> _canexecute;

		public RelayGesture(Action<GestureResult, object> execute, Func<GestureResult, object, bool> predicate = null)
		{
			this._execute = execute;
			_canexecute = predicate;
		}

		public void ExecuteGesture(GestureResult result, object annoyingbaseobjectthing)
		{
			if (_execute != null) _execute(result, annoyingbaseobjectthing);
		}

		public bool CanExecuteGesture(GestureResult result, object annoyingbaseobjectthing)
		{
			return (_canexecute == null || _canexecute(result, annoyingbaseobjectthing));
		}
	}
}
