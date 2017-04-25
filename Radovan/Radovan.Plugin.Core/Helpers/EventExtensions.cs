using System;
namespace Radovan.Plugin.Core.Helpers
{
	public static class EventExtensions
	{
		public static void Invoke<T>(this EventHandler<EventArgs<T>> handler, object sender, T value)
		{
			var handle = handler;
			if (handle != null)
			{
				handle(sender, new EventArgs<T>(value));
			}
		}

		public static bool TryInvoke<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
		{
			var handle = handler;
			if (handle == null) return false;

			handle(sender, args);
			return true;
		}
	}
}
