using System.Linq;
using UnityEngine;

namespace SpaceAttackers.Aliens
{
	public class VerticalRowMessenger : MonoBehaviour
	{
		private GroupMovement _groupMovement;

		private void Awake()
		{
			_groupMovement = transform.parent.GetComponent<GroupMovement>();
		}

		public void AlienDeactivated()
		{
			var activeChildren = GetActiveChildrenCount();
			if (activeChildren == 0)
			{
				_groupMovement.CalculateCurrentBounds();
			}
		}

		private int GetActiveChildrenCount()
		{
			return transform.Cast<Transform>().Count(child => child.gameObject.activeInHierarchy);
		}
	}
}