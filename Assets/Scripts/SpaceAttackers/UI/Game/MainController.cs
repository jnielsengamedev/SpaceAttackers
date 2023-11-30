using SpaceAttackers.UI.Game.Views;

namespace SpaceAttackers.UI.Game
{
	public class MainController : BaseController
	{
		public override void Awake()
		{
			base.Awake();
			var hudInstance = new HUD(VisualElements["HUD"], this);
			Views.Add("HUD", hudInstance);
			InitializeViews();
			RegisterInitialView(Views["HUD"]);
		}
	}
}