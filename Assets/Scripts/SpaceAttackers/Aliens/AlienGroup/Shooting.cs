﻿using System.Collections;
using System.Linq;
using SpaceAttackers.Aliens.VerticalRow;
using UnityEngine;

namespace SpaceAttackers.Aliens.AlienGroup
{
	public class Shooting : MonoBehaviour
	{
		private VerticalRowMessenger[] _verticalRows;
		private bool _coroutineRunning;
		private bool _shootingSessionInitiated;

		private void Awake()
		{
			_verticalRows = transform.Cast<Transform>().Select(child => child.GetComponent<VerticalRowMessenger>())
				.ToArray();
		}

		private VerticalRowMessenger GetRandomVerticalRow()
		{
			return _verticalRows[Random.Range(0, _verticalRows.Length)];
		}

		private void Start()
		{
			StartCoroutine(ShootingCoroutine());
		}

		private IEnumerator ShootingCoroutine()
		{
			_coroutineRunning = true;
			var endOfFrame = new WaitForEndOfFrame();
			while (_coroutineRunning)
			{
				if (_shootingSessionInitiated)
				{
					yield return endOfFrame;
					continue;
				}

				var verticalRow = GetRandomVerticalRow();
				verticalRow.Shoot();
				_shootingSessionInitiated = true;
			}
		}

		public void LaserDestroyed()
		{
			_shootingSessionInitiated = false;
		}
	}
}