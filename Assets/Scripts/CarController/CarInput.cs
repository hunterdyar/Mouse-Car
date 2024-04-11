using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MouseCar
{
	public class CarInput : MonoBehaviour
	{
		private CarController _carController;
		[Header("Input")]
		public InputActionReference Gas;
		public InputActionReference Steering;
		public InputActionReference Brake;

		private void Awake()
		{
			_carController = GetComponent<CarController>();
			
			Gas.action.Enable();
			Steering.action.Enable();
			Brake.action.Enable();
		}

		private void Update()
		{
			//gas goes from 1 as up to -1 in down
			var t = Gas.action.ReadValue<float>();
			//t = 1 - ((t + 1) * +.5f);
			_carController.SetThrottle(1-t);
			
			_carController.SetSteering(-Steering.action.ReadValue<float>());
			var b = Brake.action.ReadValue<float>();
			//b = ((b + 1) / 2);
			_carController.SetBrake(1-b);
		}
	}
}