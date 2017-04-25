using System;
namespace Radovan.Plugin.Core.Behaviors
{
	public interface IGesture
	{
		void ExecuteGesture(GestureResult result, object param);

		bool CanExecuteGesture(GestureResult result, object param);
	}
}
