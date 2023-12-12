using System;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	[RequireComponent(typeof(Animator))]
	public class LoadingScreen : MonoBehaviour
	{
		private Animator _animator;
		private Action _callback;
		[SerializeField] private GameObject moonCamera;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		public void ShowLoadingScreen(Action callback)
		{
			moonCamera.SetActive(true);
			_callback = callback;
			_animator.SetTrigger("ShowLoadingScreen");
		}

		public void HideLoadingScreen()
		{
			moonCamera.SetActive(true);
			_animator.SetTrigger("HideLoadingScreen");
		}

		public void ExecuteCallback()
		{
			_callback?.Invoke();
		}

		public void UnpauseGame()
		{
			moonCamera.SetActive(false);
			if (!PauseManager.Singleton) return;
			PauseManager.Singleton.UnpauseGame();
		}
	}
}