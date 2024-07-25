using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robile
{
	public class SpriteView : MonoBehaviour
	{
		public Transform spriteParent;

		// This sets where to place the sprite between the camera and this object's position
		// Set this below 1 to avoid clipping into the ground, depending on your character's pivot point
		// 0 = camera position
		// 1 = object position
		[Range(0f, 1f)]
		public float scaling = 1;

		private Transform _t;
		private Transform _camera;
		private ViewDirection _facing = ViewDirection.Down;
		private ViewDirection _lastFacing = ViewDirection.Down;
		private ViewDirection _lastDirection = ViewDirection.Down;

		public ViewDirection Direction { get; private set; }

		void OnEnable()
		{
			CameraView.onDirectionChanged += DirectionChanged;
		}

		void OnDisable()
		{
			CameraView.onDirectionChanged -= DirectionChanged;
		}

		void Awake()
		{
			_t = transform;
		}

		void Start()
		{
			_camera = Camera.main.transform;
		}

		void Update()
		{
			// Set the facing direction of the gameobject using the Y rotation angle
			_facing = RobitTools.GetDirection(_t.rotation.eulerAngles.y);

			if(_lastFacing != _facing)
			{
				_lastFacing = _facing;
				DirectionChanged(_lastDirection);
			}
		}

		void LateUpdate()
		{
			// Position the sprite parent away from the ground
			// This allows sprites to have pivot points above the bottom of the sprite (such as the middle of the feet, slightly above the toes)
			// The sprite's toes won't clip into the terrain because it's actually floating in the air, but gives the illusion of being grounded (shadow projectors help a lot with this)
			spriteParent.position = Vector3.Lerp(_camera.position, _t.position, scaling / 1f);

			// We scale the sprite so it's always the same size, no matter the positioning between the camera
			spriteParent.localScale = new Vector3(scaling, scaling, scaling);

			// Force the sprite to face the camera at all times
			spriteParent.LookAt(spriteParent.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up);
		}

		// This method sets the Direction of the SpriteView relative to the sprite's facing ViewDirection and the camera's ViewDirection
		// The Direction value is used to set the correct animation on the sprite
		private void DirectionChanged(ViewDirection cameraDirection)
		{
			_lastDirection = cameraDirection;

			int offset = _facing - cameraDirection;

			if(offset < 0)
			{
				offset += 8;
			}

			Direction = (ViewDirection)offset;
		}
	}
}