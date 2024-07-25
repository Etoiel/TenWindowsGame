using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robile
{
	public class AutoGround : MonoBehaviour
	{
		void Start()
		{
			RaycastHit hit;

			if(Physics.Raycast(new Ray(transform.position + Vector3.up * 10, Vector3.down), out hit))
			{
				transform.position = hit.point;
			}
		}
	}
}