using System;
using System.Collections;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class EnemyLaserManager : MonoBehaviour
	{
		public static EnemyLaserManager Singleton;
		[SerializeField] private GameObject laserPrefab;
		[SerializeField] private Aliens.AlienGroup.Shooting alienShooting;

		private void Awake()
		{
			Singleton = this;
		}

		private void OnDestroy()
		{
			Singleton = null;
		}

		public void ShootLaser(Vector3 position)
		{
			var laser = Instantiate(laserPrefab, position, laserPrefab.transform.rotation)
				.GetComponent<Laser.Movement>();
			StartCoroutine(CheckIfLaserStillExists(laser));
		}

		private IEnumerator CheckIfLaserStillExists(Laser.Movement laser)
		{
			while (!laser.beingDestroyed)
			{
				yield return new WaitForEndOfFrame();
			}

			alienShooting.LaserDestroyed();
		}
	}
}