using System;
using System.Collections.Generic;
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

		private static string RenderScalePercentage(float renderScale) =>
			$"{Math.Round(renderScale * 100, 2)}%";

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
			if (!UnsupportedPlatforms.IsUnsupportedPlatform)
			{
				var resolutionIsEmpty = _settingsData.resolution.Equals(new Resolution());
				_resolutions = Screen.resolutions.Select(resolution => (Resolution)resolution).ToList();
				if (!_resolutions.Contains(_settingsData.resolution) && !resolutionIsEmpty)
					_resolutions.Add(_settingsData.resolution);
				var resolutionIndex =
					resolutionIsEmpty
						? _resolutions.IndexOf((Resolution)Screen.currentResolution)
						: _resolutions.IndexOf(_settingsData.resolution);
				_resolution.choices = _resolutions.Select(resolution => resolution.ToString()).ToList();
				_resolution.index = resolutionIndex;
				_vsync.value = _settingsData.vsync;
				_windowed.value = _settingsData.windowed;
			}
			else
			{
				_resolution.SetEnabled(false);
				_vsync.SetEnabled(false);
				_windowed.SetEnabled(false);
			}

			if (_settingsData.qualityPreset == QualityPresets.Default)
			{
				_qualityPreset.index = QualitySettings.GetQualityLevel();
			}
			else
			{
				_qualityPreset.index = (int)_settingsData.qualityPreset;
			}

			_fsr.value = _settingsData.fsr;
			_renderScaleSlider.value = _settingsData.renderScale;
			_renderScaleLabel.text = RenderScalePercentage(_settingsData.renderScale);
			_apply.SetEnabled(false);
		}

		public override void RegisterEvents()
		{
			if (!UnsupportedPlatforms.IsUnsupportedPlatform)
			{
				_resolution.RegisterValueChangedCallback(ResolutionValueChanged);
				_vsync.RegisterValueChangedCallback(VSyncValueChanged);
				_windowed.RegisterValueChangedCallback(WindowedValueChanged);
			}

			_qualityPreset.RegisterValueChangedCallback(QualityPresetValueChanged);
			_fsr.RegisterValueChangedCallback(FsrValueChanged);
			_renderScaleSlider.RegisterValueChangedCallback(RenderScaleValueChanged);
			_apply.clicked += Apply;
			_back.clicked += Back;
		}

		public override void UnregisterEvents()
		{
			if (!UnsupportedPlatforms.IsUnsupportedPlatform)
			{
				_resolution.UnregisterValueChangedCallback(ResolutionValueChanged);
				_vsync.UnregisterValueChangedCallback(VSyncValueChanged);
				_windowed.UnregisterValueChangedCallback(WindowedValueChanged);
			}

			_qualityPreset.UnregisterValueChangedCallback(QualityPresetValueChanged);
			_fsr.UnregisterValueChangedCallback(FsrValueChanged);
			_renderScaleSlider.UnregisterValueChangedCallback(RenderScaleValueChanged);
			_apply.clicked -= Apply;
			_back.clicked -= Back;
		}

		protected override void ViewShown()
		{
			Focus();
		}

		private void Focus()
		{
			if (!UnsupportedPlatforms.IsUnsupportedPlatform)
			{
				_resolution.Focus();
			}
			else
			{
				_qualityPreset.Focus();
			}
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
			_renderScaleLabel.text = RenderScalePercentage(evt.newValue);
			CheckIfSettingsDataChanged();
		}

		private void CheckIfSettingsDataChanged()
		{
			_apply.SetEnabled(!_settingsData.Equals(_graphicsSettingsManager.Data));
		}

		private void Apply()
		{
			ApplyGraphicsSettings.Apply(_settingsData);
			_graphicsSettingsManager.SetData(_settingsData);
			_settingsData = _graphicsSettingsManager.Data.ShallowClone();
			_apply.SetEnabled(false);
			_back.Focus();
		}

		private void Back()
		{
			Controller.SwitchView("Settings");
		}
	}
}