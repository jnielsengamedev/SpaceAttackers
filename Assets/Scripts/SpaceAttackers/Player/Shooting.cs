using UnityEngine;

namespace SpaceAttackers.Player
{
	[RequireComponent(typeof(Input))]
	public class Shooting : MonoBehaviour
	{
		private Input _input;

		[SerializeField] private GameObject laser;
		[SerializeField] private Transform shootingPosition;

		private void Awake()
		{
			_input = GetComponent<Input>();
			_input.ShootingFired += ShootLaser;
		}

		private void ShootLaser()
		{
			Instantiate(laser, shootingPosition.position, laser.transform.rotation);
		}
	}
}