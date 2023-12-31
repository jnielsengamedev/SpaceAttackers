﻿using System;
using SpaceAttackers.Data;
using UnityEngine;
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
		private AudioSettingsDataManager _audioSettings;


		private static string VolumePercentage(float volume) =>
			$"{Math.Round(volume * 100, 2)}%";

		public AudioSettings(VisualElement element, BaseController controller) : base(element, controller)
		{
		}

		public override void GetElements()
		{
			_scrollView = MainElement.Q<ScrollView>();
			_volume = _scrollView.Q("Volume");
			_volumeSlider = _volume.Q<Slider>();
			_volumeLabel = _volume.Q<Label>("VolumeLabel");
			_back = MainElement.Q<Button>("Back");
			_audioSettings = new AudioSettingsDataManager();
			_volumeSlider.value = _audioSettings.Data.volume;
			_volumeLabel.text = VolumePercentage(_audioSettings.Data.volume);
		}

		public override void RegisterEvents()
		{
			_volumeSlider.RegisterValueChangedCallback(VolumeChanged);
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

		private void VolumeChanged(ChangeEvent<float> evt)
		{
			var data = _audioSettings.Data;
			data.volume = evt.newValue;
			_audioSettings.SetData(data);
			AudioListener.volume = evt.newValue;
			_volumeLabel.text = VolumePercentage(evt.newValue);
		}

		private void Back()
		{
			Controller.SwitchView("Settings");
		}
	}
}