using System;
using UnityEngine;

namespace MouseCar
{
	
	public class CarController : MonoBehaviour
	{
		public Wheel LeftWheel;
		public Wheel RightWheel;
		public WheelCollider FrontWheel;
		
		private Rigidbody _rigidbody;
		[SerializeField] private Transform _centerOfMass;
		[SerializeField] private float engineTorque;
		[SerializeField] private float brakeTorque;
		private float _throttle;
		private float _brake;
		private float _steering;
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.centerOfMass = _centerOfMass.position;
		}

		public void SetThrottle(float throttle)
		{
			//lol it's all rigged on -z
			_throttle = -throttle;
		}

		public void SetBrake(float brake)
		{
			
			_brake = brake;
		}

		public void SetSteering(float steer)
		{
			//oops it's rigged on -z
			_steering = -steer;
		}
		private void FixedUpdate()
		{
			AnimateWheels();
			ApplyEngine();
		}

		private void ApplyEngine()
		{
			float t = _throttle * engineTorque;
			//float l = _steering >= 0 ? 100 : Mathf.InverseLerp(0, -1, _steering);
			//float r = _steering <= 0 ? 100 : Mathf.InverseLerp(0, 1, _steering);
			RightWheel.Collider.motorTorque =  t;
			LeftWheel.Collider.motorTorque = t;
			
			//brake should be 0 to like 0.5 up, then .5 to 1 back down
			//then reverse throttle from .5 to 1 down.
			LeftWheel.Collider.brakeTorque = _brake * brakeTorque;
			RightWheel.Collider.brakeTorque = _brake * brakeTorque;
			
			//Apply Steering
			var s = _steering;//0.5 is straight.
			LeftWheel.Collider.steerAngle = -Mathf.Lerp(-45, 45, s);
			RightWheel.Collider.steerAngle = -Mathf.Lerp(-45, 45, s);
			FrontWheel.steerAngle = -Mathf.Lerp(45, -45,s);
		}

		void AnimateWheels()
		{
			LeftWheel.Collider.GetWorldPose(out var pos, out var rot);
			LeftWheel.Model.transform.position = pos;
			LeftWheel.Model.transform.rotation = rot;
			RightWheel.Collider.GetWorldPose(out pos, out rot);
			RightWheel.Model.transform.position = pos;
			RightWheel.Model.transform.rotation = rot;
		}
	}
}