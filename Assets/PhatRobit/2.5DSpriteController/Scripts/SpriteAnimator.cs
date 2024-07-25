using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robile
{
	public class SpriteAnimator : MonoBehaviour
	{
		public Animator animator;

		private SpriteView _view;
		
		void Awake()
		{
			_view = GetComponent<SpriteView>();
		}

		void Update()
		{
			// Set the Animator's Direction float parameter to match that of the SpriteView's ViewDirection
			// This allows us to use a BlendTree to easily animate all 8 directions
			animator.SetFloat("Direction", (int)_view.Direction);
		}
	}
}