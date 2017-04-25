using System;
using System.Threading.Tasks;

namespace Radovan.Plugin.Core.Behaviors
{
	public class RelayGestureAsync<T> : IGesture where T : class
	{
		private readonly Func<GestureResult, T, Task> _asyncExecute;
		private readonly Func<GestureResult, T, bool> _canexecute;

		public RelayGestureAsync(Func<GestureResult, T, Task> execute, Func<GestureResult, T, bool> predicate)
		{
			_asyncExecute = execute;
			_canexecute = predicate;
		}

		public async void ExecuteGesture(GestureResult result, object param)
		{
			if (_asyncExecute != null) await Execute(result, param);
		}

		public bool CanExecuteGesture(GestureResult result, object annoyingbaseobjectthing)
		{
			return (_canexecute == null || _canexecute(result, annoyingbaseobjectthing as T));
		}

		protected virtual async Task Execute(GestureResult gesture, object annoyingbaseobjectthing)
		{
			await _asyncExecute(gesture, annoyingbaseobjectthing as T);
		}

	}

	public class RelayGestureAsync : IGesture
	{
		private readonly Func<GestureResult, object, Task> _asyncExecute;
		private readonly Func<GestureResult, object, bool> _canexecute;

		public RelayGestureAsync(Func<GestureResult, object, Task> execute, Func<GestureResult, object, bool> predicate)
		{
			_asyncExecute = execute;
			_canexecute = predicate;
		}

		public async void ExecuteGesture(GestureResult result, object param)
		{
			if (_asyncExecute != null) await Execute(result, param);
		}

		public bool CanExecuteGesture(GestureResult result, object annoyingbaseobjectthing)
		{
			return (_canexecute == null || _canexecute(result, annoyingbaseobjectthing));
		}

		protected virtual async Task Execute(GestureResult gesture, object annoyingbaseobjectthing)
		{
			await _asyncExecute(gesture, annoyingbaseobjectthing);
		}
	}
}
