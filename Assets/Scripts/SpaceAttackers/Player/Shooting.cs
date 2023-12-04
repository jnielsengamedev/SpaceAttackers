using UnityEngine;

namespace SpaceAttackers.Player
{
	[RequireComponent(typeof(Input))]
	public class Shooting : MonoBehaviour
	{
		private Input _input;

		[SerializeField] private GameObject laserPrefab;
		[SerializeField] private Transform shootingPosition;

		private void Awake()
		{
			_input = GetComponent<Input>();
			_input.ShootingFired += ShootLaser;
		}

		private void ShootLaser()
		{
			Instantiate(laserPrefab, shootingPosition.position, laserPrefab.transform.rotation);
		}
	}
}