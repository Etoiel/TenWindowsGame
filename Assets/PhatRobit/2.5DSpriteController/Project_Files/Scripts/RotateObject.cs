using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robile
{
	public class RotateObject : MonoBehaviour
	{
		public float speed = 1;

		private Transform _t;

		void Awake()
		{
			_t = transform;
		}

		void Update()
		{
			_t.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
		}
	}
}