using System.Linq;
using SpaceAttackers.Aliens.AlienGroup;
using UnityEngine;

namespace SpaceAttackers.Aliens.VerticalRow
{
	public class VerticalRowMessenger : MonoBehaviour
	{
		private GroupMovement _groupMovement;
		private Shooting _shooting;
		private Alien.AlienMessenger[] _aliens;

		private void Awake()
		{
			_groupMovement = transform.parent.GetComponent<GroupMovement>();
			_shooting = transform.parent.GetComponent<Shooting>();
			_aliens = transform.Cast<Transform>().Select(child => child.GetComponent<Alien.AlienMessenger>()).ToArray();
		}

		public void Shoot()
		{
			var activeAliens = _aliens.Where(child => child.gameObject.activeInHierarchy).ToArray();
			var randomAlien = activeAliens[Random.Range(0, activeAliens.Length)];
			randomAlien.ShootLaser();
		}

		public void AlienDeactivated()
		{
			var activeChildren = GetActiveChildrenCount();
			if (activeChildren == 0)
			{
				_groupMovement.CalculateCurrentBounds();
			}
		}

		private int GetActiveChildrenCount()
		{
			return _aliens.Count(child => child.gameObject.activeInHierarchy);
		}

		public void LaserDestroyed()
		{
			_shooting.LaserDestroyed();
		}
	}
}