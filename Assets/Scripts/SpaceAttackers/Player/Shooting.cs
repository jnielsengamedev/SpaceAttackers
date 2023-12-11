using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Player
{
	public class Shooting : MonoBehaviour
	{
		[SerializeField] private GameObject laserPrefab;
		[SerializeField] private Transform shootingPosition;

		private void Start()
		{
			InputManager.Singleton.ShootingFired += ShootLaser;
		}

		private void ShootLaser()
		{
			if (PauseManager.Singleton.IsPaused) return;
			Instantiate(laserPrefab, shootingPosition.position, laserPrefab.transform.rotation);
		}
	}
}