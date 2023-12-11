using System;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Player
{
	[RequireComponent(typeof(AudioSource))]
	public class Shooting : MonoBehaviour
	{
		[SerializeField] private GameObject laserPrefab;
		[SerializeField] private Transform shootingPosition;
		[SerializeField] private AudioClip laserSound;
		private AudioSource _audioSource;


		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void Start()
		{
			InputManager.Singleton.ShootingFired += ShootLaser;
		}

		private void ShootLaser()
		{
			if (PauseManager.Singleton.IsPaused) return;
			Instantiate(laserPrefab, shootingPosition.position, laserPrefab.transform.rotation);
			_audioSource.PlayOneShot(laserSound);
		}
	}
}