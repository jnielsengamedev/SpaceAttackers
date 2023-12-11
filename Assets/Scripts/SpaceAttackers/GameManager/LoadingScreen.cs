using System;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	[RequireComponent(typeof(Animator))]
	public class LoadingScreen : MonoBehaviour
	{
		private Animator _animator;
		private Action _callback;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		public void ShowLoadingScreen(Action callback)
		{
			_callback = callback;
			_animator.SetTrigger("ShowLoadingScreen");
		}

		public void HideLoadingScreen()
		{
			_animator.SetTrigger("HideLoadingScreen");
		}

		public void ExecuteCallback()
		{
			_callback?.Invoke();
		}

		public void UnpauseGame()
		{
			if (!PauseManager.Singleton) return;
			PauseManager.Singleton.UnpauseGame();
		}
	}
}