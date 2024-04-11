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
		[SerializeField, Range(0, 1)] private float brakeToReversePoint;
		private float _throttle;
		private float _brake;
		private float _realBrake;
		private float _steering;
		private float _reverse;
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
			_realBrake = 1-(Mathf.Abs(_brake - brakeToReversePoint)*(1/brakeToReversePoint));
			LeftWheel.Collider.brakeTorque = _realBrake * brakeTorque;
			RightWheel.Collider.brakeTorque = _realBrake * brakeTorque;
			_reverse = Mathf.InverseLerp(brakeToReversePoint, 1, _brake);
			if (_reverse > 0 && _throttle < 0.75f)
			{
				RightWheel.Collider.motorTorque = _reverse*0.75f*engineTorque;
				LeftWheel.Collider.motorTorque = _reverse * 0.75f*engineTorque;
			}

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