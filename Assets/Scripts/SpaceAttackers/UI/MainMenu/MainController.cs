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
			Views.Add("MainScreen", mainScreenInstance);
			InitializeViews();
			RegisterInitialView(Views["MainScreen"]);
		}
	}
}