using System.Collections;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	[RequireComponent(typeof(AudioSource))]
	public class EnemyLaserManager : MonoBehaviour
	{
		public static EnemyLaserManager Singleton;
		[SerializeField] private GameObject laserPrefab;
		[SerializeField] private Aliens.AlienGroup.Shooting alienShooting;
		[SerializeField] private AudioClip enemyLaserSound;
		private AudioSource _audioSource;

		private void Awake()
		{
			Singleton = this;
			_audioSource = GetComponent<AudioSource>();
		}

		private void OnDestroy()
		{
			Singleton = null;
		}

		public void ShootLaser(Vector3 position)
		{
			var laser = Instantiate(laserPrefab, position, laserPrefab.transform.rotation)
				.GetComponent<Laser.Movement>();
			_audioSource.PlayOneShot(enemyLaserSound);
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