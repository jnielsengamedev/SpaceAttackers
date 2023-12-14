using System;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class Lives : MonoBehaviour
	{
		[SerializeField] private long lives;

		public static Lives Singleton;
		public static long PlayerLives { get; private set; }

		public event Action<long> LivesUpdated;

		private void Awake()
		{
			Singleton = this;
			PlayerLives = lives;
		}

		private void OnDestroy()
		{
			Singleton = null;
		}

		public void SubtractLife()
		{
			PlayerLives -= 1;
			LivesUpdated?.Invoke(PlayerLives);
		}

		public void DeleteLives()
		{
			PlayerLives = 0;
			LivesUpdated?.Invoke(PlayerLives);
		}

		public void AddLife()
		{
			PlayerLives += 1;
			LivesUpdated?.Invoke(PlayerLives);
		}
	}
}