using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;


public class MouseControl : MonoBehaviour
{
	private Camera _camera;


	private Vector2Int screenPos;
	[DllImport("User32.dll")]
	static extern uint SendInput(uint nInputs, INPUT[] inputs, int cbSize);
	[DllImport("User32.dll")]
	static extern bool SetCursorPos(int x, int y);

	private void Start()
	{
		_camera = Camera.main;
	}
	
	
	private void Update()
	{
		screenPos = ScreenPosition();
		SetCursorPos(screenPos.x, screenPos.y);
	
		//looks like raw windows input can get thing while it's running in the "bg"
		//but the goal is to use a racing wheel, which might use different input systems anyway. 
		//https://github.com/elringus/unity-raw-input
		
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Click();
		}
	}

	private void Click()
	{
		INPUT down = new INPUT()
		{
			type = InputType.INPUT_MOUSE,
			U = new InputUnion()
			{
				mi = new MOUSEINPUT()
				{
					dx = screenPos.x,
					dy = screenPos.y,
					dwFlags = MOUSEEVENTF.LEFTDOWN
				}
			}
		};
		INPUT up = new INPUT()
		{
			type = InputType.INPUT_MOUSE,
			U = new InputUnion()
			{
				mi = new MOUSEINPUT()
				{
					dx = screenPos.x,
					dy = screenPos.y,
					dwFlags = MOUSEEVENTF.LEFTUP
				}
			}
		};
		SendInput(2, new INPUT[] { down, up }, INPUT.Size);
	}

	Vector2Int ScreenPosition()
	{
		var spos = _camera.WorldToScreenPoint(transform.position);
		return new Vector2Int(Mathf.RoundToInt(spos.x), Screen.height - Mathf.RoundToInt(spos.y));
	}
}
