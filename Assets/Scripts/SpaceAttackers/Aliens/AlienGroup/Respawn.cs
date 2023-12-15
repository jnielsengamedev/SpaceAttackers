using System.Collections;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Aliens.AlienGroup
{
	[RequireComponent(typeof(GroupMovement))]
	[RequireComponent(typeof(Shooting))]
	public class Respawn : MonoBehaviour
	{
		private GroupMovement _movement;
		private Shooting _shooting;

		private void Awake()
		{
			_movement = GetComponent<GroupMovement>();
			_shooting = GetComponent<Shooting>();
		}

		public void RespawnAliens()
		{
			StartCoroutine(_movement.StopMovement());
			Lives.Singleton.AddLife();
			StartCoroutine(WaitingCoroutine());
		}

		private IEnumerator WaitingCoroutine()
		{
			yield return new WaitForSeconds(0.5f);
			_movement.AddSpeed();
			yield return StartCoroutine(_movement.StartMovement());
			_shooting.StartShooting();
			PauseManager.Singleton.UnpauseGame();
		}
	}
}