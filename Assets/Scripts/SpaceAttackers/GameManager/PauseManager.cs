using System;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class PauseManager : MonoBehaviour
	{
		public bool IsPaused { get; private set; }
		public static PauseManager Singleton;
		[SerializeField] private LoadingScreen loadingScreen;

		private void Awake()
		{
			Singleton = this;
			IsPaused = true;
		}

		private void OnDestroy()
		{
			Singleton = null;
		}

		private void Start()
		{
			loadingScreen.HideLoadingScreen();
		}

		public void PauseGame()
		{
			IsPaused = true;
		}

		public void UnpauseGame()
		{
			IsPaused = false;
		}
	}
}