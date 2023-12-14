using SpaceAttackers.Data;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class ApplySettings : MonoBehaviour
	{
		private static bool _initialized;

		private void Awake()
		{
			if (_initialized) return;
			var graphicsSettingsManager = new GraphicsSettingsDataManager();
			var audioSettingsManager = new AudioSettingsDataManager();
			ApplyGraphicsSettings.Apply(graphicsSettingsManager.Data);
			AudioListener.volume = audioSettingsManager.Data.volume;
			_initialized = true;
		}
	}
}