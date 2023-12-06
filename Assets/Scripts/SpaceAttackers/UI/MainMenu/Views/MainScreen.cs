using SpaceAttackers.GameManager;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace SpaceAttackers.UI.MainMenu.Views
{
	public class MainScreen : View
	{
		private Button _start;
		private LoadingScreen _loadingScreen;

		public MainScreen(VisualElement element, BaseController controller, LoadingScreen loadingScreen) : base(element,
			controller)
		{
			_loadingScreen = loadingScreen;
		}

		public override void GetElements()
		{
			_start = MainElement.Q<Button>("Start");
		}

		public override void RegisterEvents()
		{
			_start.clicked += Start;
		}

		public override void UnregisterEvents()
		{
			_start.clicked -= Start;
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
	}
}