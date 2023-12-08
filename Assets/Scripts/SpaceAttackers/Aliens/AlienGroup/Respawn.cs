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
		private Lives.AddLife _addLife;

		private void Awake()
		{
			_movement = GetComponent<GroupMovement>();
			_shooting = GetComponent<Shooting>();
			_addLife = Lives.Singleton.AskForAddLife(gameObject);
		}

		public void RespawnAliens()
		{
			StartCoroutine(_movement.StopMovement());
			_addLife();
			StartCoroutine(WaitingCoroutine());
		}

		private IEnumerator WaitingCoroutine()
		{
			yield return new WaitForSeconds(0.5f);
			yield return StartCoroutine(_movement.StartMovement());
			_shooting.StartShooting();
			PauseManager.Singleton.UnpauseGame();
		}
	}
}