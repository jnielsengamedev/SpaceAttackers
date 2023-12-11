using System.Collections;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Player
{
	[RequireComponent(typeof(Renderer))]
	[RequireComponent(typeof(AudioSource))]
	public class Lives : MonoBehaviour
	{
		private GameManager.Lives.SubtractLife _subtractLife;
		[SerializeField] private ParticleSystem explosion;
		[SerializeField] private GameOverScreen gameOverScreen;
		[SerializeField] private AudioClip explosionSound;
		private Renderer _renderer;
		private AudioSource _audioSource;

		private void Awake()
		{
			_subtractLife = GameManager.Lives.Singleton.AskForSubtractLife(gameObject);
			_renderer = GetComponent<Renderer>();
			_audioSource = GetComponent<AudioSource>();
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
			_audioSource.PlayOneShot(explosionSound);
			yield return new WaitForSeconds(0.03f);
			_renderer.enabled = false;
			while (explosion.isPlaying)
			{
				yield return new WaitForEndOfFrame();
			}

			if (GameManager.Lives.PlayerLives <= 0)
			{
				gameOverScreen.ShowGameOverScreen();
				yield break;
			}

			transform.position = new Vector3(0, 0.25f, 0);
			_renderer.enabled = true;
			PauseManager.Singleton.UnpauseGame();
		}
	}
}