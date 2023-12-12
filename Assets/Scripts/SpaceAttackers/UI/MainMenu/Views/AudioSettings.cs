using UnityEngine.UIElements;

namespace SpaceAttackers.UI.MainMenu.Views
{
	public class AudioSettings : View
	{
		private ScrollView _scrollView;
		private VisualElement _volume;
		private Slider _volumeSlider;
		private Label _volumeLabel;
		private Button _back;
		
		public AudioSettings(VisualElement element, BaseController controller) : base(element, controller)
		{
		}

		public override void GetElements()
		{
			_scrollView = MainElement.Q<ScrollView>();
			_volume = _scrollView.Q("Volume");
			_volumeSlider = _volume.Q<Slider>();
			_volumeLabel = _volume.Q<Label>();
			_back = MainElement.Q<Button>("Back");
		}

		public override void RegisterEvents()
		{
			_back.clicked += Back;
		}

		public override void UnregisterEvents()
		{
			_back.clicked -= Back;
		}

		protected override void ViewShown()
		{
			_volumeSlider.Focus();
		}

		private void Back()
		{
			Controller.SwitchView("Settings");
		}
	}
}