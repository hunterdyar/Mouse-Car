using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class WorldBounds : MonoBehaviour
	{
		private Camera _camera;
		
		private void Start()
		{
			_camera = Camera.main;
			CreateColliders();
		}

		[ContextMenu("Create Colliders")]
		public void CreateColliders()
		{
			ClearChildren();
			float thick = 1;

			transform.position = _camera.transform.position;
			transform.rotation = _camera.transform.rotation;
			float depth = 0.5f;
			var bottomLeftW = _camera.ScreenToWorldPoint(new Vector3(0, 0, depth));
			var topRightW = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0));

			bottomLeftW = transform.InverseTransformVector(bottomLeftW);
			topRightW = transform.InverseTransformVector(topRightW);
			//work in local space will keep bounds axis-aligned.
			var center = Vector3.Lerp(bottomLeftW, topRightW, .5f);
			var size = topRightW - bottomLeftW;
			
			size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.y));

			//Create Walls
			var right = new GameObject().AddComponent<BoxCollider>();
			right.gameObject.name = "right";
			right.transform.SetParent(transform);
			right.transform.localPosition = Vector3.forward * depth / 2 + Vector3.right * size.x / 2;
			right.center = new Vector3(thick / 2, 0, 0);
			//right.transform.localScale = new Vector3(thick, size.z + thick, size.y);
			right.size = new Vector3(thick, size.y, size.z+thick);
			
			var left = new GameObject().AddComponent<BoxCollider>(); 
			left.gameObject.name = "left"; 
			left.transform.SetParent(transform); 
			left.transform.localPosition = Vector3.forward * depth / 2 + Vector3.left * size.x / 2; 
			left.center = new Vector3(-thick / 2, 0, 0); 
			left.size = new Vector3(thick, size.y, size.z+thick);
			//left.transform.localScale = new Vector3(thick,  size.z + thick, size.y);
			
			var up = new GameObject().AddComponent<BoxCollider>();
			up.gameObject.name = "up";
			up.transform.SetParent(transform);
			up.transform.localPosition = Vector3.forward * depth / 2 + Vector3.up * size.y / 2;
			up.center = new Vector3(0, 0, -thick / 2);
			up.size = new Vector3(size.x+thick, size.y, thick);
			
			var down = new GameObject().AddComponent<BoxCollider>(); 
			down.gameObject.name = "down"; 
			down.transform.SetParent(transform); 
			down.transform.localPosition = Vector3.forward * depth / 2 + Vector3.down * size.y / 2;
			down.center = new Vector3(0, 0, thick / 2); 
			down.size = new Vector3(size.x+thick, size.y, thick);
		}

		private void ClearChildren()
		{
			foreach (Transform child in transform)
			{
				Destroy(child.gameObject);
			}
		}
	}
}