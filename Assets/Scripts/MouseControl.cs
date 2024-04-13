using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.UIElements.Cursor;


public class MouseControl : MonoBehaviour
{
	public static event Action<int> OnClickHappened;
	private Camera _camera;
	public InputActionReference LeftClickInput;
	public InputActionReference RightClickInput;

	private Vector2Int screenPos;
	[DllImport("User32.dll")]
	static extern uint SendInput(uint nInputs, INPUT[] inputs, int cbSize);
	[DllImport("User32.dll")]
	static extern bool SetCursorPos(int x, int y);

	public bool enableInEditor = true;
	private void Start()
	{
		//Cursor.visible = false;
		_camera = Camera.main;
		LeftClickInput.action.Enable();
		RightClickInput.action.Enable();
	}
	
	
	private void Update()
	{
		if (!enableInEditor)
		{
#if UNITY_EDITOR
			return;
#endif
		}
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
			OnClickHappened?.Invoke(0);
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
			OnClickHappened?.Invoke(0);
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
