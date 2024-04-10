
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class Windowless : MonoBehaviour
{
	[DllImport("User32.dll")]
	private static extern IntPtr GetActiveWindow();

	[DllImport("User32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int index, uint dwNewLong);

	[DllImport("User32.dll", SetLastError = true)]
	static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags); 
	
	private struct Margins
	{
		public int cxLeftWidth;
		public int cxRightWidth;
		public int cyTopHeight;
		public int cyBottomHeight;
	}

	[DllImport("Dwmapi.dll")]
	private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins margins);

	private const int GWL_EXStyle = -20;
	private const uint WS_EX_LAYERED = 0x00080000;
	private const uint WS_EX_TRANSPARENT = 0x00000020;
	private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
	
	private void Start()
	{
#if !UNITY_EDITOR
		IntPtr window = GetActiveWindow();
		Margins margins = new Margins { cxLeftWidth = -1 };
		DwmExtendFrameIntoClientArea(window, ref margins);
		SetWindowLong(window, GWL_EXStyle, WS_EX_LAYERED | WS_EX_TRANSPARENT);
		SetWindowPos(window, HWND_TOPMOST, 0, 0, 0, 0, 0);
#endif
	}

}
