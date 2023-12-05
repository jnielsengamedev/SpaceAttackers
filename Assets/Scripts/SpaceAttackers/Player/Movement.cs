using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Player
{
	[RequireComponent(typeof(Input))]
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;
		private Input _input;
		private Camera _camera;

		private void Awake()
		{
			_input = GetComponent<Input>();
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

			transform.Translate(Vector3.right * (_input.HorizontalInput * moveSpeed * Time.deltaTime));
		}

		private void SetPosition(float cameraEdge)
		{
			var originalPosition = transform.position;
			originalPosition.x = cameraEdge;
			transform.position = originalPosition;
		}
	}
}