using System;
using SpaceAttackers.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceAttackers.GameManager
{
	public class InputManager : MonoBehaviour
	{
		private GameInputAction _gameInputAction;
		public static InputManager Singleton;
		public float HorizontalInput { get; private set; }
		public event Action ShootingFired;

		private void Awake()
		{
			Singleton = this;
			_gameInputAction = new GameInputAction();
			EnableActions();
		}

		private void OnDestroy()
		{
			Singleton = null;
			_gameInputAction.Spaceship.Movement.Disable();
			_gameInputAction.Spaceship.Shooting.performed -= ShotsFired;
			_gameInputAction.Spaceship.Shooting.Disable();
		}

		private void EnableActions()
		{
			_gameInputAction.Spaceship.Movement.Enable();
			_gameInputAction.Spaceship.Shooting.Enable();

			_gameInputAction.Spaceship.Shooting.performed += ShotsFired;
		}

		private void ShotsFired(InputAction.CallbackContext context)
		{
			ShootingFired?.Invoke();
		}

		private void Update()
		{
			GetHorizontalMovement();
		}

		private void GetHorizontalMovement()
		{
			HorizontalInput = _gameInputAction.Spaceship.Movement.ReadValue<float>();
		}
	}
}