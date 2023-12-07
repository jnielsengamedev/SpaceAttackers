using System.Collections;
using System.Linq;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Aliens.AlienGroup
{
	[RequireComponent(typeof(GroupMovement))]
	[RequireComponent(typeof(Shooting))]
	public class Respawn : MonoBehaviour
	{
		private GameObject[] _verticalRows;
		private GroupMovement _movement;
		private Shooting _shooting;
		private Lives.AddLife _addLife;

		private void Awake()
		{
			_verticalRows = transform.Cast<Transform>().Select(child => child.gameObject)
				.ToArray();
			_movement = GetComponent<GroupMovement>();
			_shooting = GetComponent<Shooting>();
			_addLife = Lives.Singleton.AskForAddLife(gameObject);
		}

		public void RespawnAliens()
		{
			StartCoroutine(_movement.StopMovement());
			
			foreach (var verticalRow in _verticalRows)
			{
				verticalRow.SetActive(true);
			}
			
			_addLife();
			StartCoroutine(WaitingCoroutine());
		}

		private IEnumerator WaitForPositionToChange()
		{
			while (_movement.gameObject.transform.position != new Vector3(0, 2, 0))
			{
				yield return new WaitForEndOfFrame();
			}
		}

		private IEnumerator WaitingCoroutine()
		{
			yield return new WaitForSeconds(0.5f);
			_movement.StartMovement();
			_shooting.StartShooting();
			PauseManager.Singleton.UnpauseGame();
		}
	}
}