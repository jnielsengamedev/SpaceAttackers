using SpaceAttackers.GameManager;
using SpaceAttackers.Input;
using SpaceAttackers.UI.MainMenu.Views;
using UnityEngine;

namespace SpaceAttackers.UI.MainMenu
{
	public class MainController : BaseController
	{
		[SerializeField] private LoadingScreen loadingScreen;
		private GameInputAction _inputAction;

		public override void Awake()
		{
			base.Awake();
			_inputAction = new GameInputAction();
			_inputAction.UI.Enable();
			var mainScreenInstance = new MainScreen(VisualElements["MainScreen"], this, loadingScreen, _inputAction);
			Views.Add("MainScreen", mainScreenInstance);
			InitializeViews();
			RegisterInitialView(Views["MainScreen"]);
		}
	}
}