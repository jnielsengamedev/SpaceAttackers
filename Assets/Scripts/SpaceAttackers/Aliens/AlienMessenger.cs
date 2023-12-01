using UnityEngine;

namespace SpaceAttackers.Aliens
{
	public class AlienMessenger : MonoBehaviour
	{
		private VerticalRowMessenger _verticalRow;

		private void Awake()
		{
			_verticalRow = transform.parent.GetComponent<VerticalRowMessenger>();
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
			_verticalRow.AlienDeactivated();
		}
	}
}