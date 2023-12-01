using SpaceAttackers.Aliens;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Laser
{
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;
		private Camera _camera;
		private Score.AddScore _addScore;

		private void Awake()
		{
			_camera = Camera.main;
			_addScore = Score.Singleton.AskForAddScore(gameObject);
		}

		private void Update()
		{
			var cameraEdge = _camera.orthographicSize * 2 + 1;

			if (transform.position.y > Mathf.Abs(cameraEdge))
			{
				Destroy(gameObject);
			}

			transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Enemy")) return;

			var scoreAmountExists = other.TryGetComponent<Aliens.ScoreAmount>(out var amount);
			_addScore(scoreAmountExists ? amount.scoreAmount : 20);

			other.gameObject.GetComponent<AlienMessenger>().Deactivate();
			Destroy(gameObject);
		}
	}
}