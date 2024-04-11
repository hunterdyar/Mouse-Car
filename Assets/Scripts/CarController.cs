using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
	public class CarController : MonoBehaviour
	{
		[Header("Input")]
		public InputAction Gas;
		public InputAction Steering;
		public InputAction Brake;

		[Header("Car Settings")]
		public float SteerForce;
		public float engineForce;
		
		[Header("Debug")]
		[SerializeField] private float _gas;
		[SerializeField] private float _steering;
		[SerializeField] private float _brake;

		
		private Rigidbody2D _rb;

		private void Awake()
		{
			Gas.Enable();
			Steering.Enable();
			Brake.Enable();
			_rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			//gas goes from 1 as up to -1 in down
			_gas = Gas.ReadValue<float>();
			_gas = 1-((_gas + 1) * +.5f);
			_steering = -Steering.ReadValue<float>();
			_brake = Brake.ReadValue<float>();
			_brake = ((_brake+1) / 2);

		}

		public void FixedUpdate()
		{
			
			float forward = engineForce * _gas;
			_rb.AddForce(transform.up *forward);
			_rb.AddForce(-(transform.up*_brake*0.9f));
			_rb.AddTorque(_steering*SteerForce);
				
		}
	}
}