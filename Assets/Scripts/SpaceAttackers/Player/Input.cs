using System;
using SpaceAttackers.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceAttackers.Player
{
	public class Input : MonoBehaviour
	{
		private GameInputAction _gameInputAction;
		public float HorizontalInput { get; private set; }
		public event Action ShootingFired;

		private void Awake()
		{
			_gameInputAction = new GameInputAction();
			EnableActions();
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