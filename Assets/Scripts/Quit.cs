using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
	public class Quit : MonoBehaviour
	{
		public InputActionReference QuitAction;

		private void Awake()
		{
			QuitAction.action.Enable();
		}

		private void Update()
		{
			if (QuitAction.action.WasPerformedThisFrame())
			{
				Application.Quit();
			}
		}
	}
}