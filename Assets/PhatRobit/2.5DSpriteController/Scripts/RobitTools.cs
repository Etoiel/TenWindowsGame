using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robile
{
	public static class RobitTools
	{
		// Returns the correct ViewDirection of an object based on the given angle (usually the transform's Y rotation)
		public static ViewDirection GetDirection(float angle)
		{
			if(angle < -180) angle += 360;
			else if(angle > 180) angle -= 360;

			float x = Mathf.Abs(angle);

			if(x < 22.5f) return ViewDirection.Up;
			else if(x < 67.5f) return angle < 0 ? ViewDirection.UpLeft : ViewDirection.UpRight;
			else if(x < 112.5f) return angle < 0 ? ViewDirection.Left : ViewDirection.Right;
			else if(x < 157.5f) return angle < 0 ? ViewDirection.DownLeft : ViewDirection.DownRight;

			return ViewDirection.Down;
		}
		
		public static void ClampAngle(ref Vector3 angle)
		{
			if(angle.x < -180) angle.x += 360;
			else if(angle.x > 180) angle.x -= 360;

			if(angle.y < -180) angle.y += 360;
			else if(angle.y > 180) angle.y -= 360;

			if(angle.z < -180) angle.z += 360;
			else if(angle.z > 180) angle.z -= 360;
		}
	}
}