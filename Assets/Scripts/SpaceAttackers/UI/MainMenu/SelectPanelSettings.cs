using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceAttackers.UI.MainMenu
{
	public class SelectPanelSettings : MonoBehaviour
	{
		private EventSystem _eventSystem;
		private GameObject _panelSettings;

		private void Awake()
		{
			_eventSystem = GetComponent<EventSystem>();
		}

		private void Start()
		{
			_panelSettings = transform.GetChild(0).gameObject;
			_eventSystem.SetSelectedGameObject(_panelSettings);
		}
	}
}