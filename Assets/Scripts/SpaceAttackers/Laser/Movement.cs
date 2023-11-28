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
			var viewportPoint = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));

			if (transform.position.y > Mathf.Abs(viewportPoint.z))
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