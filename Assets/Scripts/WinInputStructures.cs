using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct INPUT
{
	public InputType type;
	public InputUnion U;

	internal static int Size
	{
		get { return Marshal.SizeOf(typeof(INPUT)); }
	}
}

public enum InputType : uint
{
	INPUT_MOUSE = 0,
	INPUT_KEYBOARD = 1,
	INPUT_HARDWARE = 2,
}

[StructLayout(LayoutKind.Explicit)]
public struct InputUnion
{
	[FieldOffset(0)] internal MOUSEINPUT mi;
	//[FieldOffset(0)] internal KEYBOARDINPUT ki;
	[FieldOffset(0)] internal HARDWAREINPUT hi;
}

[StructLayout(LayoutKind.Sequential)]
public struct MOUSEINPUT
{
	internal int dx;
	internal int dy;
	internal int mouseData;
	internal MOUSEEVENTF dwFlags;
	internal uint time;
	internal UIntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Sequential)]
public struct HARDWAREINPUT
{
	internal int uMsg;
	internal short wParamL;
	internal short wParamH;
}

[Flags]
public enum MOUSEEVENTF : uint
{
	ABSOLUTE = 0x8000,
	HWHEEL = 0x01000,
	MOVE = 0x0001,
	MOVE_NOCOALESCE = 0x2000,
	LEFTDOWN = 0x0002,
	LEFTUP = 0x0004,
	RIGHTDOWN = 0x0008,
	RIGHTUP = 0x0010,
	MIDDLEDOWN = 0x0020,
	MIDDLEUP = 0x0040,
	VIRTUALDESK = 0x4000,
	WHEEL = 0x0800,
	XDOWN = 0x0080,
	XUP = 0x0100
}