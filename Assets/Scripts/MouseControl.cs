using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class MouseControl : MonoBehaviour
{
	private Camera _camera;
	public InputActionReference LeftClickInput;
	public InputActionReference RightClickInput;

	private Vector2Int screenPos;
	[DllImport("User32.dll")]
	static extern uint SendInput(uint nInputs, INPUT[] inputs, int cbSize);
	[DllImport("User32.dll")]
	static extern bool SetCursorPos(int x, int y);

	private void Start()
	{
		_camera = Camera.main;
		LeftClickInput.action.Enable();
		RightClickInput.action.Enable();
	}
	
	
	private void Update()
	{
		screenPos = ScreenPosition();
		SetCursorPos(screenPos.x, screenPos.y);
	
		//looks like raw windows input can get thing while it's running in the "bg"
		//but the goal is to use a racing wheel, which might use different input systems anyway. 
		//https://github.com/elringus/unity-raw-input
		
		
		if (LeftClickInput.action.WasPressedThisFrame())
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
			Click(down);
		}else if (LeftClickInput.action.WasReleasedThisFrame())
		{
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
			Click(up);
		}
		//right click
		if (RightClickInput.action.WasPressedThisFrame())
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
						dwFlags = MOUSEEVENTF.RIGHTUP
					}
				}
			};
			Click(down);
		}
		else if (RightClickInput.action.WasReleasedThisFrame())
		{
			INPUT up = new INPUT()
			{
				type = InputType.INPUT_MOUSE,
				U = new InputUnion()
				{
					mi = new MOUSEINPUT()
					{
						dx = screenPos.x,
						dy = screenPos.y,
						dwFlags = MOUSEEVENTF.RIGHTUP
					}
				}
			};
			Click(up);
		}
	}

	private void Click(INPUT input)
	{
		SendInput(2, new INPUT[] { input }, INPUT.Size);
	}

	Vector2Int ScreenPosition()
	{
		var spos = _camera.WorldToScreenPoint(transform.position);
		return new Vector2Int(Mathf.RoundToInt(spos.x), Screen.height - Mathf.RoundToInt(spos.y));
	}
}
