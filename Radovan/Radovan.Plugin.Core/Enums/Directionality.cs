using System;
namespace Radovan.Plugin.Core.Enums
{
	[Flags]
	public enum Directionality
	{
		None = 0,
		Left = 0x01,
		Right = 0x02,
		Up = 0x10,
		Down = 0x20,
		HorizontalMask = 0x0F,
		VerticalMask = 0xF0
	}
}
