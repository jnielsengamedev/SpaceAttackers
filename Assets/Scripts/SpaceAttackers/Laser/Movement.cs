using UnityEngine;

namespace SpaceAttackers.Laser
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
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}