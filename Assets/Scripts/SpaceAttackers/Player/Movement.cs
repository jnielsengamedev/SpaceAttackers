using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Player
{
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;
		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			if (PauseManager.Singleton.IsPaused) return;

			var cameraEdge = _camera.orthographicSize * _camera.aspect / 2 + 3f;

			if (transform.position.x < -cameraEdge)
			{
				SetPosition(-cameraEdge);
			}

			if (transform.position.x > cameraEdge)
			{
				SetPosition(cameraEdge);
			}

			transform.Translate(Vector3.right * (InputManager.Singleton.HorizontalInput * moveSpeed * Time.deltaTime));
		}

		private void SetPosition(float cameraEdge)
		{
			var originalPosition = transform.position;
			originalPosition.x = cameraEdge;
			transform.position = originalPosition;
		}
	}
}