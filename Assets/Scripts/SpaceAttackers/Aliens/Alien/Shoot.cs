using UnityEngine;

namespace SpaceAttackers.Aliens.Alien
{
	public class Shoot : MonoBehaviour
	{
		[SerializeField] private GameObject laserPrefab;

		internal GameObject ShootLaser()
		{
			return Instantiate(laserPrefab, transform.position, laserPrefab.transform.rotation);
		}
	}
}