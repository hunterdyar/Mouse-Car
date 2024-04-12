using System;
using System.Collections;
using System.Collections.Generic;
using DirectInputManager;
using Unity.VisualScripting;
using UnityEngine;

namespace MouseCar
{
	public class Feedback : MonoBehaviour
	{
		// DIManager.
		public string _device;
		private bool _hasDevice = false;
		private Coroutine bonkRoutine;
		private void Start()
		{
			if(!DIManager.isInitialized){
				DIManager.Initialize();
			}

			_hasDevice = false;
			foreach (var d in DIManager.devices)
			{
				if (d.FFBCapable)
				{
					_device = d.guidInstance;
					_hasDevice = true;
					break;
				}
			}

			if (_hasDevice)
			{
				DIManager.EnableFFBEffect(_device, FFBEffects.Damper);
				DIManager.EnableFFBEffect(_device, FFBEffects.ConstantForce);
				DIManager.EnableFFBEffect(_device, FFBEffects.Spring);
				
				//
				DIManager.UpdateDamperSimple(_device, 0);
				DIManager.UpdateSpringSimple(_device, 0, 0, 400, 400, 46000, 4600);
			}
		}

		private void OnDestroy()
		{
			if (!string.IsNullOrEmpty(_device))
			{
				DIManager.StopAllFFBEffects(_device);
			}
		}

		//bleh not working
		// private void OnCollisionEnter(Collision other)
		// {
		// 	if (_hasDevice)
		// 	{
		// 		if (bonkRoutine != null)
		// 		{
		// 			StopCoroutine(bonkRoutine);
		// 		}
		//
		// 		bonkRoutine = StartCoroutine(Bonk());
		// 	}
		// }

		private IEnumerator Bonk()
		{
			DIManager.UpdateConstantForceSimple(_device, 2);
			yield return new WaitForSeconds(0.05f);
			DIManager.UpdateConstantForceSimple(_device, -2);
			yield return new WaitForSeconds(0.05f);
			DIManager.UpdateConstantForceSimple(_device, 2);
			yield return new WaitForSeconds(0.05f);
			DIManager.UpdateConstantForceSimple(_device, -2);
			yield return new WaitForSeconds(0.05f);
			DIManager.UpdateConstantForceSimple(_device, 0);
		}
	}
}