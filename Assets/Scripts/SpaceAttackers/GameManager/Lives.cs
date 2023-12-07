using System;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class Lives : MonoBehaviour
	{
		[SerializeField] private long lives;

		public static Lives Singleton;
		public static long PlayerLives { get; private set; }

		public delegate void SubtractLife();

		public delegate void AddLife();

		public event Action<long> LivesUpdated;

		private void Awake()
		{
			Singleton = this;
			PlayerLives = lives;
		}

		private void SubtractLifeImplementation()
		{
			PlayerLives -= 1;
			LivesUpdated?.Invoke(PlayerLives);
		}

		private void AddLifeImplementation()
		{
			PlayerLives += 1;
			LivesUpdated?.Invoke(PlayerLives);
		}

		public SubtractLife AskForSubtractLife(GameObject gameObj)
		{
			if (gameObj.CompareTag("Player")) return SubtractLifeImplementation;

			return () => { };
		}

		public AddLife AskForAddLife(GameObject gameObj)
		{
			if (gameObj.CompareTag("AlienGroup")) return AddLifeImplementation;
			return () => { };
		}
	}
}