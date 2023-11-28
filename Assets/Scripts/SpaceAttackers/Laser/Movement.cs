using UnityEngine;

namespace SpaceAttackers.Laser
{
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;

		private void Update()
		{
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