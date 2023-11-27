using SpaceAttackers.Input;
using UnityEngine;

namespace SpaceAttackers.Player
{
	public class Input : MonoBehaviour
	{
		private GameInputAction _gameInputAction;
		public float HorizontalInput { get; private set; }
		private void Awake()
		{
			_gameInputAction = new GameInputAction();
			EnableActions();
		}

		private void EnableActions()
		{
			_gameInputAction.Spaceship.Movement.Enable();
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