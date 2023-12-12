using UnityEngine.UIElements;

namespace SpaceAttackers.UI.MainMenu.Views
{
	public class GraphicsSettings : View
	{
		private ScrollView _scrollView;
		private DropdownField _resolution;
		private Toggle _vsync;
		private Toggle _windowed;
		private DropdownField _qualityPreset;
		private Toggle _fsr;
		private VisualElement _renderScale;
		private Slider _renderScaleSlider;
		private Label _renderScaleLabel;
		private Button _apply;
		private Button _back;


		public GraphicsSettings(VisualElement element, BaseController controller) : base(element, controller)
		{
		}

		public override void GetElements()
		{
			_scrollView = MainElement.Q<ScrollView>();
			_resolution = _scrollView.Q<DropdownField>("Resolution");
			_vsync = _scrollView.Q<Toggle>("VSync");
			_windowed = _scrollView.Q<Toggle>("Windowed");
			_qualityPreset = _scrollView.Q<DropdownField>("QualityPreset");
			_fsr = _scrollView.Q<Toggle>("FSR");
			_renderScale = _scrollView.Q<VisualElement>("RenderScale");
			_renderScaleSlider = _renderScale.Q<Slider>();
			_renderScaleLabel = _renderScale.Q<Label>();
			_apply = MainElement.Q<Button>("Apply");
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
			_resolution.Focus();
		}

		private void Back()
		{
			Controller.SwitchView("Settings");
		}
	}
}