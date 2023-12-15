using UnityEngine.UIElements;

namespace SpaceAttackers.UI.MainMenu.Views
{
	public class Settings : View
	{
		private ScrollView _scrollView;
		private Button _graphics;
		private Button _audio;
		private Button _back;

		public Settings(VisualElement element, BaseController controller) : base(element, controller)
		{
		}

		public override void GetElements()
		{
			_scrollView = MainElement.Q<ScrollView>();
			_graphics = _scrollView.Q<Button>("Graphics");
			_audio = _scrollView.Q<Button>("Audio");
			_back = MainElement.Q<Button>("Back");
		}

		public override void RegisterEvents()
		{
			_graphics.clicked += Graphics;
			_audio.clicked += Audio;
			_back.clicked += Back;
		}

		public override void UnregisterEvents()
		{
			_graphics.clicked -= Graphics;
			_audio.clicked -= Audio;
			_back.clicked -= Back;
		}

		protected override void ViewShown()
		{
			_graphics.Focus();
		}

		private void Graphics()
		{
			Controller.SwitchView("GraphicsSettings");
		}

		private void Audio()
		{
			Controller.SwitchView("AudioSettings");
		}

		private void Back()
		{
			Controller.SwitchView("MainScreen");
		}
	}
}