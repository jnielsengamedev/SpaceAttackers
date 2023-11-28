using UnityEngine;

namespace SpaceAttackers
{
	public class MoonRotation : MonoBehaviour
	{
		private void Update()
		{
			transform.Rotate(Vector3.down, 5 * Time.deltaTime);
		}
	}
}