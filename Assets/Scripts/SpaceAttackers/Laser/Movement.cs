using SpaceAttackers.Aliens.Alien;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Laser
{
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;
		private Camera _camera;
		private Score.AddScore _addScore;
		public bool beingDestroyed;

		private void Awake()
		{
			_camera = Camera.main;
			_addScore = Score.Singleton.AskForAddScore(gameObject);
		}

		private void Update()
		{
			if (transform.position.y is > 12 or < -12)
			{
				Destroy(gameObject);
			}


			transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
		}

		private void OnTriggerEnter(Collider other)
		{
			switch (other.tag)
			{
				case "Enemy":
					EnemyCollision(other);
					break;
				case "Player":
					if (CompareTag("Laser")) return;
					PlayerCollision(other);
					break;
			}
		}

		private void EnemyCollision(Component other)
		{
			var scoreAmountExists = other.TryGetComponent<Aliens.ScoreAmount>(out var amount);
			_addScore(scoreAmountExists ? amount.scoreAmount : 20);
			other.gameObject.GetComponent<AlienMessenger>().Deactivate();
			Destroy(gameObject);
		}

		private void PlayerCollision(Collider other)
		{
			print("ouchie, player has been hit");
			Destroy(gameObject);
		}

		private void OnDestroy()
		{
			beingDestroyed = true;
		}
	}
}