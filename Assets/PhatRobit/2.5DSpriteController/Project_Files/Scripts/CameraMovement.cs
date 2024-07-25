using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//applesauce
//applesnap

namespace Robile
{
	public class CameraMovement : MonoBehaviour
	{
		[Header("CameraControlls")]
		public Transform target;


		public float rotationSmoothing = 10;
		public float rotationSensitivity = 8;

		public float minDistance = 3;
		public float maxDistance = 10;
		public float minAngle = 10;
		public float maxAngle = 50;
		public float speedAngle = .25f;
		public float zoomStep = 1;
		public float zoomSpeed = 0.25f;
		public AnimationCurve zoomCurve;

		private Vector3 _angle = new Vector3();
		private float _wantedDistance = 0;
		private float _lastDistance = 0;
		private float _distance = 0;
		private float _zoomTime = 0;

		private Transform _t;

		private GameUnityInputSystem _input;
		private InputAction _lookAction;
		private InputAction _zoomAction;

		public Vector2 CurrentRotation { get { return _angle; } }


		void Awake()
		{
			_t = transform;
			_distance = maxDistance;
			_wantedDistance = _distance;
			_lastDistance = _distance;
			_input = new GameUnityInputSystem();
		}

		void OnEnable()
		{
			_input.Player.Look.Enable();
			_input.Player.Zoom.Enable();
			_input.Player.CameraRotate.Enable();
			_input.Player.CameraAngle.Enable();
			_lookAction = _input.Player.Look;
			_zoomAction = _input.Player.Zoom;
		}

		private void OnDisable()
		{
			_input.Player.Look.Disable();
			_input.Player.Zoom.Disable();
			_input.Player.CameraRotate.Disable();
			_input.Player.CameraAngle.Disable();
		}

		void Update()
		{
			if(target)
			{
				// Get the Horizontal Mouse Input while the Right Mouse Button is pressed
				if(_input.Player.CameraRotate.IsPressed())
				{
					_angle.x += _lookAction.ReadValue<Vector2>().x * rotationSensitivity;
					RobitTools.ClampAngle(ref _angle);
				}

				if(_input.Player.CameraAngle.IsPressed())
				{
    				_angle.y -= _lookAction.ReadValue<Vector2>().y * speedAngle;
    				_angle.y = Mathf.Clamp(_angle.y, minAngle, maxAngle);
    				RobitTools.ClampAngle(ref _angle);
				}
				

				// Get the Mouse Wheel input to zoom
				float wheel = -Mathf.Clamp(_zoomAction.ReadValue<Vector2>().y * 9999, -1, 1);

				if(wheel != 0)
				{
					_lastDistance = _distance;
					_wantedDistance = Mathf.Clamp(_wantedDistance + zoomStep * wheel, minDistance, maxDistance);
					_zoomTime = 0;
				}

				// Smoothly zoom the camera in / out
				if(_zoomTime < zoomSpeed)
				{
					_zoomTime = Mathf.Clamp(_zoomTime + Time.deltaTime, 0, zoomSpeed);
					_distance = Mathf.Lerp(_lastDistance, _wantedDistance, zoomCurve.Evaluate(_zoomTime / zoomSpeed));
				}
			}
		}

		void LateUpdate()
		{
			if(target)
			{
				// Position the camera based on the input, distance and angles
				Quaternion currentRotation = Quaternion.Euler(_angle.y, _angle.x, 0);

				_t.position = target.position - currentRotation * Vector3.forward * _distance;
				_t.LookAt(target.position, Vector3.up);
			}
		}
	}
}