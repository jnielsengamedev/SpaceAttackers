using System;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class Score : MonoBehaviour
	{
		public static Score Singleton;
		public static long GameScore { get; private set; }

		public delegate void AddScore(long score);

		public event Action<long> ScoreUpdated;

		private void Awake()
		{
			Singleton = this;
			GameScore = 0;
		}

		private void AddScoreImplementation(long score)
		{
			GameScore += score;
			ScoreUpdated?.Invoke(GameScore);
		}

		public AddScore AskForAddScore(GameObject gameObj)
		{
			if (gameObj.CompareTag("Laser"))
			{
				return AddScoreImplementation;
			}

			return _ => { };
		}
	}
}