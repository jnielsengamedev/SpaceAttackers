using SpaceAttackers.GameManager;
using SpaceAttackers.UI.MainMenu.Views;
using UnityEngine;

namespace SpaceAttackers.UI.MainMenu
{
	public class MainController : BaseController
	{
		[SerializeField] private LoadingScreen loadingScreen;

		public override void Awake()
		{
			base.Awake();
			var mainScreenInstance = new MainScreen(VisualElements["MainScreen"], this, loadingScreen);
			var settingsInstance = new Settings(VisualElements["Settings"], this);
			var graphicsSettingsInstance = new GraphicsSettings(VisualElements["GraphicsSettings"], this);
			var audioSettingsInstance = new Views.AudioSettings(VisualElements["AudioSettings"], this);
			Views.Add("MainScreen", mainScreenInstance);
			Views.Add("Settings", settingsInstance);
			Views.Add("GraphicsSettings", graphicsSettingsInstance);
			Views.Add("AudioSettings", audioSettingsInstance);
			InitializeViews();
			RegisterInitialView(Views["MainScreen"]);
		}

		private void Start()
		{
			loadingScreen.HideLoadingScreen();
		}
	}
}