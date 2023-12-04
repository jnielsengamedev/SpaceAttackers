using System.Collections;
using SpaceAttackers.Aliens.VerticalRow;
using UnityEngine;

namespace SpaceAttackers.Aliens.Alien
{
	[RequireComponent(typeof(Shoot))]
	public class AlienMessenger : MonoBehaviour
	{
		private VerticalRowMessenger _verticalRow;
		private Shoot _shoot;

		private void Awake()
		{
			_verticalRow = transform.parent.GetComponent<VerticalRowMessenger>();
			_shoot = GetComponent<Shoot>();
		}

		public void ShootLaser()
		{
			var laser = _shoot.ShootLaser().GetComponent<Laser.Movement>();
			StartCoroutine(CheckIfLaserStillExists(laser));
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
			_verticalRow.AlienDeactivated();
		}

		private IEnumerator CheckIfLaserStillExists(Laser.Movement laser)
		{
			while (!laser.beingDestroyed)
			{
				yield return new WaitForEndOfFrame();
			}

			_verticalRow.LaserDestroyed();
		}
	}
}