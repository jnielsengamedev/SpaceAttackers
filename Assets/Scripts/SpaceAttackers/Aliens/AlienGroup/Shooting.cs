using System.Collections;
using System.Linq;
using SpaceAttackers.Aliens.VerticalRow;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Aliens.AlienGroup
{
	[RequireComponent(typeof(Respawn))]
	public class Shooting : MonoBehaviour
	{
		private VerticalRowMessenger[] _verticalRows;
		private Respawn _respawn;
		private bool _coroutineRunning;
		private bool _shootingSessionInitiated;
		private static readonly WaitForEndOfFrame EndOfFrame = new();
		private static readonly WaitForSeconds HalfSecond = new(0.5f);

		private void Awake()
		{
			_verticalRows = transform.Cast<Transform>().Select(child => child.GetComponent<VerticalRowMessenger>())
				.ToArray();
			_respawn = GetComponent<Respawn>();
		}

		private VerticalRowMessenger GetRandomVerticalRow()
		{
			var activeRows = _verticalRows.Where(child => child.gameObject.activeInHierarchy).ToArray();
			
			if (activeRows.Length == 0)
			{
				return null;
			}

			return activeRows[Random.Range(0, activeRows.Length)];
		}

		private void Start()
		{
			StartCoroutine(ShootingCoroutine());
		}

		private IEnumerator ShootingCoroutine()
		{
			_coroutineRunning = true;
			while (_coroutineRunning)
			{
				if (PauseManager.Singleton.IsPaused)
				{
					yield return EndOfFrame;
					if (_coroutineRunning == false)
					{
						break;
					}
					continue;
				}
				
				if (_shootingSessionInitiated)
				{
					yield return EndOfFrame;
					continue;
				}
				
				yield return HalfSecond;
				var verticalRow = GetRandomVerticalRow();
				if (!verticalRow)
				{
					_coroutineRunning = false;
					continue;
				}
				verticalRow.Shoot();
				_shootingSessionInitiated = true;
			}
			
			_respawn.RespawnAliens();
		}

		public void LaserDestroyed()
		{
			_shootingSessionInitiated = false;
		}

		public void StartShooting()
		{
			StartCoroutine(ShootingCoroutine());
		}
	}
}