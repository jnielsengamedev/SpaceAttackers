using System.Collections;
using SpaceAttackers.Aliens.VerticalRow;
using UnityEngine;

namespace SpaceAttackers.Aliens.Alien
{
	[RequireComponent(typeof(Renderer))]
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(AudioSource))]
	public class AlienMessenger : MonoBehaviour
	{
		private VerticalRowMessenger _verticalRow;
		[SerializeField] private ParticleSystem explosion;
		[SerializeField] private AudioClip explosionSound;
		private Renderer _renderer;
		private Collider _collider;
		private AudioSource _audioSource;
		public bool isActive = true;

		private void Awake()
		{
			_verticalRow = transform.parent.GetComponent<VerticalRowMessenger>();
			_renderer = GetComponent<Renderer>();
			_collider = GetComponent<Collider>();
			_audioSource = GetComponent<AudioSource>();
		}

		private void OnEnable()
		{
			_renderer.enabled = true;
			_collider.enabled = true;
			isActive = true;
		}

		public void ShootLaser()
		{
			GameManager.EnemyLaserManager.Singleton.ShootLaser(transform.position);
		}

		public void Deactivate()
		{
			isActive = false;
			StartCoroutine(ExplosionCoroutine());
		}

		private IEnumerator ExplosionCoroutine()
		{
			_collider.enabled = false;
			explosion.Play();
			_audioSource.PlayOneShot(explosionSound);
			yield return new WaitForSeconds(0.03f);
			_renderer.enabled = false;
			while (explosion.isPlaying)
			{
				yield return new WaitForEndOfFrame();
			}

			gameObject.SetActive(false);
			_verticalRow.AlienDeactivated();
		}
	}
}