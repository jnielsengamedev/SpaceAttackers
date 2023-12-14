using SpaceAttackers.Data;
using UnityEngine;

namespace SpaceAttackers.GameManager
{
	public class ApplySettings : MonoBehaviour
	{
		private void Awake()
		{
			var graphicsSettingsManager = new GraphicsSettingsDataManager();
			var audioSettingsManager = new AudioSettingsDataManager();
			ApplyGraphicsSettings.Apply(graphicsSettingsManager.Data);
			AudioListener.volume = audioSettingsManager.Data.volume;
		}
	}
}