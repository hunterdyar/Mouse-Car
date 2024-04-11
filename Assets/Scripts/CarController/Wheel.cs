using UnityEngine;

namespace MouseCar
{
	[System.Serializable]
	public struct Wheel
	{
		public Transform Model;
		public WheelCollider Collider;
	}
}