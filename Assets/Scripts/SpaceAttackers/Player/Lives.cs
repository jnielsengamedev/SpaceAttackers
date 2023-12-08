using System.Collections;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Player
{
	[RequireComponent(typeof(Renderer))]
	public class Lives : MonoBehaviour
	{
		private GameManager.Lives.SubtractLife _subtractLife;
		[SerializeField] private ParticleSystem explosion;
		private Renderer _renderer;

		private void Awake()
		{
			_subtractLife = GameManager.Lives.Singleton.AskForSubtractLife(gameObject);
			_renderer = GetComponent<Renderer>();
		}

		public void PlayerShot()
		{
			_subtractLife();
			PauseManager.Singleton.PauseGame();
			StartCoroutine(ExplosionCoroutine());
		}

		private IEnumerator ExplosionCoroutine()
		{
			explosion.Play();
			yield return new WaitForSeconds(0.03f);
			_renderer.enabled = false;
			while (explosion.isPlaying)
			{
				yield return new WaitForEndOfFrame();
			}

			if (GameManager.Lives.PlayerLives <= 0)
			{
				// GameOverScreen.Singleton.ShowGameOverScreen();
				yield break;
			}

			transform.position = new Vector3(0, 0.25f, 0);
			_renderer.enabled = true;
			PauseManager.Singleton.UnpauseGame();
		}
	}
}