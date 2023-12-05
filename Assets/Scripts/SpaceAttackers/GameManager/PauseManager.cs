using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class PauseManager : MonoBehaviour
	{
		public bool IsPaused { get; private set; }
		public static PauseManager Singleton;

		private void Awake()
		{
			Singleton = this;
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