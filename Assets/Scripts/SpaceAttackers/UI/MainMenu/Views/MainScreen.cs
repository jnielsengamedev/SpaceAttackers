using System.Collections.Generic;
using SpaceAttackers.GameManager;
using SpaceAttackers.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SpaceAttackers.UI.MainMenu.Views
{
	public class MainScreen : View
	{
		private Button _start;
		private Button _settings;
		private Button _quit;

		private readonly List<VisualElement> _focusableElements = new();
		private int _currentFocusedElement;
		private readonly LoadingScreen _loadingScreen;
		private readonly GameInputAction _input;

		public MainScreen(VisualElement element, BaseController controller, LoadingScreen loadingScreen,
			GameInputAction input) : base(element,
			controller)
		{
			_loadingScreen = loadingScreen;
			_input = input;
		}

		public override void GetElements()
		{
			_start = MainElement.Q<Button>("Start");
			_settings = MainElement.Q<Button>("Settings");
			_quit = MainElement.Q<Button>("Quit");
			_focusableElements.Add(_start);
			_focusableElements.Add(_settings);
			_focusableElements.Add(_quit);
			_start.Focus();
		}

		public override void RegisterEvents()
		{
			MainElement.parent.RegisterCallback<NavigationMoveEvent>(e =>
			{
				e.PreventDefault();
				e.StopImmediatePropagation();
			});

			_start.clicked += Start;
			_input.UI.Submit.Enable();
			_input.UI.MoveFocus.Enable();
			_input.UI.Submit.started += ClickButton;
			_input.UI.MoveFocus.started += FocusMoved;
		}

		public override void UnregisterEvents()
		{
			_start.clicked -= Start;
			_input.UI.Submit.Disable();
			_input.UI.Submit.performed -= ClickButton;
		}

		private void Start()
		{
			Controller.UnregisterAllEvents();
			_loadingScreen.ShowLoadingScreen(LoadGame);
		}

		private void LoadGame()
		{
			SceneManager.LoadSceneAsync("Game");
		}

		private void ClickButton(InputAction.CallbackContext callbackContext)
		{
			Debug.Log("Click");
			var focusedItem = MainElement.focusController.focusedElement as Button;
			focusedItem.SendEvent(new NavigationSubmitEvent());
		}

		private void FocusMoved(InputAction.CallbackContext callbackContext)
		{
			if (!callbackContext.started) return;
			var vector = callbackContext.ReadValue<Vector2>();
			switch (vector.y)
			{
				case 1:
					if (_currentFocusedElement == 0)
					{
						_currentFocusedElement = _focusableElements.Count - 1;
					}
					else
					{
						_currentFocusedElement = _currentFocusedElement -= 1;
					}

					_focusableElements[_currentFocusedElement].Focus();
					break;
				case -1:
					_currentFocusedElement = Mathf.Abs((_currentFocusedElement += 1) % _focusableElements.Count);

					_focusableElements[_currentFocusedElement].Focus();
					break;
			}
		}
	}
}