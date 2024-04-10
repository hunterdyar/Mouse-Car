using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class CarController : MonoBehaviour
	{
		private Rigidbody2D _rb;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		public void Update()
		{
			Vector2 force = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			_rb.AddForce(force);
		}
	}
}