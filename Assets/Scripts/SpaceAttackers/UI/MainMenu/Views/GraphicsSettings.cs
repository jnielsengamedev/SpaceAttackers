using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SpaceAttackers.Data;
using UnityEngine;
using UnityEngine.UIElements;
using Resolution = SpaceAttackers.Data.Resolution;

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
		private GraphicsSettingsDataManager _graphicsSettingsManager;
		private GraphicsSettingsData _settingsData;
		private List<Resolution> _resolutions;

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
			_renderScaleLabel = _renderScale.Q<Label>("RenderScaleLabel");
			_apply = MainElement.Q<Button>("Apply");
			_back = MainElement.Q<Button>("Back");
			_graphicsSettingsManager = new GraphicsSettingsDataManager();

			PopulateElements();
		}

		private void PopulateElements()
		{
			_settingsData = _graphicsSettingsManager.Data.ShallowClone();
			var resolutionIsEmpty = _settingsData.resolution.Equals(new Resolution());
			_resolutions = Screen.resolutions.Select(UnityResolutionToGameResolution).ToList();
			if (!_resolutions.Contains(_settingsData.resolution) && !resolutionIsEmpty)
				_resolutions.Add(_settingsData.resolution);
			var resolutionIndex =
				resolutionIsEmpty
					? _resolutions.IndexOf(UnityResolutionToGameResolution(Screen.currentResolution))
					: _resolutions.IndexOf(_settingsData.resolution);
			_resolution.choices = _resolutions.Select(resolution => resolution.ToString()).ToList();
			_resolution.index = resolutionIndex;
			_vsync.value = _settingsData.vsync;
			_windowed.value = _settingsData.windowed;
			_qualityPreset.index = (int)_settingsData.qualityPreset;
			_fsr.value = _settingsData.fsr;
			_renderScaleSlider.value = _settingsData.renderScale;
			_renderScaleLabel.text = _settingsData.renderScale.ToString(CultureInfo.InvariantCulture);
			_apply.SetEnabled(false);
		}

		public override void RegisterEvents()
		{
			_resolution.RegisterValueChangedCallback(ResolutionValueChanged);
			_vsync.RegisterValueChangedCallback(VSyncValueChanged);
			_windowed.RegisterValueChangedCallback(WindowedValueChanged);
			_qualityPreset.RegisterValueChangedCallback(QualityPresetValueChanged);
			_fsr.RegisterValueChangedCallback(FsrValueChanged);
			_renderScaleSlider.RegisterValueChangedCallback(RenderScaleValueChanged);
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

		private void ResolutionValueChanged(ChangeEvent<string> evt)
		{
			_settingsData.resolution = _resolutions[_resolution.index];
			CheckIfSettingsDataChanged();
		}

		private void VSyncValueChanged(ChangeEvent<bool> evt)
		{
			_settingsData.vsync = evt.newValue;
			CheckIfSettingsDataChanged();
		}

		private void WindowedValueChanged(ChangeEvent<bool> evt)
		{
			_settingsData.windowed = evt.newValue;
			CheckIfSettingsDataChanged();
		}

		private void QualityPresetValueChanged(ChangeEvent<string> evt)
		{
			_settingsData.qualityPreset = (QualityPresets)_qualityPreset.index;
			CheckIfSettingsDataChanged();
		}

		private void FsrValueChanged(ChangeEvent<bool> evt)
		{
			_settingsData.fsr = evt.newValue;
			CheckIfSettingsDataChanged();
		}

		private void RenderScaleValueChanged(ChangeEvent<float> evt)
		{
			_settingsData.renderScale = evt.newValue;
			CheckIfSettingsDataChanged();
		}

		private void CheckIfSettingsDataChanged()
		{
			_apply.SetEnabled(!_settingsData.Equals(_graphicsSettingsManager.Data));
		}

		private void Back()
		{
			Controller.SwitchView("Settings");
		}

		private Resolution UnityResolutionToGameResolution(UnityEngine.Resolution resolution)
		{
			return new Resolution
			{
				width = resolution.width,
				height = resolution.height,
				refreshRate = resolution.refreshRateRatio.value
			};
		}
	}
}