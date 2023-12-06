using UnityEngine;

namespace SpaceAttackers
{
	public class MoonRotation : MonoBehaviour
	{
		[SerializeField] private float speed = 5;
		private static Vector3 _rotation;

		private void Awake()
		{
			if (_rotation != Vector3.zero)
			{
				transform.eulerAngles = _rotation;
			}
		}

		private void Update()
		{
			transform.Rotate(Vector3.down, speed * Time.deltaTime);
		}

		private void OnDestroy()
		{
			_rotation = transform.eulerAngles;
		}
	}
}