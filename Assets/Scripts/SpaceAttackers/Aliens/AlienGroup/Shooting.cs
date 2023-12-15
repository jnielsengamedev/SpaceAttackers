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
		private static readonly WaitForSeconds TwoSeconds = new(2);

		private void Awake()
		{
			_verticalRows = transform.Cast<Transform>().Select(child => child.GetComponent<VerticalRowMessenger>())
				.ToArray();
			_respawn = GetComponent<Respawn>();
		}


		private VerticalRowMessenger[] GetActiveRows()
		{
			return _verticalRows.Where(child => child.gameObject.activeInHierarchy).ToArray();
		}

		private VerticalRowMessenger GetRandomVerticalRow()
		{
			var activeRows = GetActiveRows();

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

				var verticalRow = GetRandomVerticalRow();
				if (!verticalRow)
				{
					_coroutineRunning = false;
					_shootingSessionInitiated = false;
					break;
				}

				verticalRow.Shoot();
				_shootingSessionInitiated = true;
				yield return TwoSeconds;
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