using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//apple
//toaster

namespace Robile
{
	public class PlayerMovement : MonoBehaviour
	{
		public float speed = 3;
		public float gravity = -9;
		public float jumpPower = 5;
		public bool canDoubleJump = true;

		private CharacterController _controller;
		private Transform _t;
		private float _velocity = 0;
		private SpriteAnimator _animator;
		private bool _doubleJumped = false;

		private GameUnityInputSystem _input;
		private InputAction _moveAction;

		void Awake()
		{
			_t = transform;
			_controller = GetComponent<CharacterController>();
			_animator = GetComponent<SpriteAnimator>();
			_input = new GameUnityInputSystem();
		}

		void OnEnable()
		{
			_input.Player.Move.Enable();
			_moveAction = _input.Player.Move;
			_input.Player.Jump.Enable();
			_input.Player.Jump.performed += OnJump;
		}

		private void OnDisable()
		{
			_input.Player.Move.Disable();
			_input.Player.Jump.Disable();
			_input.Player.Jump.performed -= OnJump;
		}

		void Update()
		{
			// Grab keyboard input
			// OLD INPUT:
			// Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			// NEW INPUT:
			Vector2 input = _moveAction.ReadValue<Vector2>();

			// If the player is moving diagonally, make sure the magnitude doesn't exceed 1
			// Without this, the player would move faster than we want them to while moving diagonally
			if(input.magnitude > 1)
			{
				input /= input.magnitude;
			}

			_animator.animator.SetBool("Walking", input != Vector2.zero);

			// Find the direction the player should be moving based on the camera's angle and the player's input
			Vector3 direction = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(input.x, 0, input.y);

			// Rotate the player to face the movement direction
			if(direction != Vector3.zero)
			{
				_t.rotation = Quaternion.LookRotation(direction);
			}

			// Apply gravity
			_velocity += gravity * Time.deltaTime;

			// Apply movement speed and move the controller
			Vector3 moveVector = direction * speed;
			moveVector.y = _velocity;
			_controller.Move(moveVector * Time.deltaTime);

			// Reset velocity if on the ground (prevents falling super fast when running off ledges)
			if(_controller.isGrounded)
			{
				_doubleJumped = false;
				_velocity = -1;
			}
		}

		private bool CanJump()
		{
			if(_controller.isGrounded)
			{
				return true;
			}

			if(canDoubleJump && !_doubleJumped)
			{
				_doubleJumped = true;
				return true;
			}

			return false;
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			if(CanJump())
			{
				_velocity = jumpPower;
				
			}
		}
	}
}