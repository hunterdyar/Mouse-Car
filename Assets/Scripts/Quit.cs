using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
	public class Quit : MonoBehaviour
	{
		public InputActionReference QuitAction;
		public InputActionReference ResetAction;
		private void Awake()
		{
			QuitAction.action.Enable();
			ResetAction.action.Enable();
		}

		private void Update()
		{
			if (QuitAction.action.WasPerformedThisFrame())
			{
				Application.Quit();
			}

			if (ResetAction.action.WasPerformedThisFrame())
			{
				SceneManager.LoadScene(0);
			}
		}
	}
}