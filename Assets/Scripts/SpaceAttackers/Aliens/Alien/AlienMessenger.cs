using SpaceAttackers.Aliens.VerticalRow;
using UnityEngine;

namespace SpaceAttackers.Aliens.Alien
{
	public class AlienMessenger : MonoBehaviour
	{
		private VerticalRowMessenger _verticalRow;

		private void Awake()
		{
			_verticalRow = transform.parent.GetComponent<VerticalRowMessenger>();
		}

		public void ShootLaser()
		{
			GameManager.EnemyLaserManager.Singleton.ShootLaser(transform.position);
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
			_verticalRow.AlienDeactivated();
		}
	}
}