using SpaceAttackers.GameManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SpaceAttackers.UI.MainMenu.Views
{
	public class MainScreen : View
	{
		private Button _start;
		private Button _settings;
		private Button _quit;

		private readonly LoadingScreen _loadingScreen;
		private bool _quitNotSupported;

		public MainScreen(VisualElement element, BaseController controller, LoadingScreen loadingScreen) : base(element,
			controller)
		{
			_loadingScreen = loadingScreen;
			_quitNotSupported = Application.platform is RuntimePlatform.WebGLPlayer or RuntimePlatform.LinuxEditor
				or RuntimePlatform.WindowsEditor or RuntimePlatform.OSXEditor;
		}

		public override void GetElements()
		{
			_start = MainElement.Q<Button>("Start");
			_settings = MainElement.Q<Button>("Settings");
			_quit = MainElement.Q<Button>("Quit");

			_start.Focus();
			_settings.SetEnabled(false);
			if (_quitNotSupported) _quit.SetEnabled(false);
		}

		public override void RegisterEvents()
		{
			_start.clicked += Start;
			if (!_quitNotSupported)
			{
				_quit.clicked += Quit;
			}
		}

		public override void UnregisterEvents()
		{
			_start.clicked -= Start;
			if (!_quitNotSupported)
			{
				_quit.clicked -= Quit;
			}
		}

		private void Start()
		{
			Controller.UnregisterAllEvents();
			_loadingScreen.ShowLoadingScreen(LoadGame);
		}

		private static void Quit()
		{
			Application.Quit();
		}

		private static void LoadGame()
		{
			SceneManager.LoadSceneAsync("Game");
		}
	}
}