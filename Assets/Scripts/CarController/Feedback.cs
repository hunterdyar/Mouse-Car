using System;
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
				DIManager.UpdateDamperSimple(_device, 0);
			}
		}

		private void OnDestroy()
		{
			if (!string.IsNullOrEmpty(_device))
			{
				DIManager.StopAllFFBEffects(_device);
			}
		}
	}
}