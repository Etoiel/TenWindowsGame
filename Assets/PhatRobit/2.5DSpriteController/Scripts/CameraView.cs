using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robile
{
	public class CameraView : MonoBehaviour
	{
		public delegate void CameraEvent(ViewDirection direction);
		public static event CameraEvent onDirectionChanged;

		private ViewDirection _direction = ViewDirection.Down;
		private ViewDirection _lastDirection = ViewDirection.Left;
		private Transform _t;

		void Awake()
		{
			_t = transform;
		}

		void LateUpdate()
		{
			// Set the current ViewDirection based on the camera's Y rotation angle (forward)
			_direction = RobitTools.GetDirection(_t.rotation.eulerAngles.y);

			// Send out the event to tell other objects that the camera's ViewDirection has changed
			if(_lastDirection != _direction)
			{
				_lastDirection = _direction;

				if(onDirectionChanged != null)
				{
					onDirectionChanged(_direction);
				}
			}
		}
	}
}