using SpaceAttackers.Data;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SpaceAttackers
{
	public static class ApplyGraphicsSettings
	{
		public static void Apply(GraphicsSettingsData settingsData)
		{
			if (settingsData.Equals(new GraphicsSettingsData())) return;
			ApplyResolution(settingsData);

			if (settingsData.qualityPreset != QualityPresets.Default)
				QualitySettings.SetQualityLevel((int)settingsData.qualityPreset);
			var qualityPreset = settingsData.qualityPreset == QualityPresets.Default
				? (UniversalRenderPipelineAsset)Object.Instantiate(QualitySettings.GetRenderPipelineAssetAt(
					QualitySettings.GetQualityLevel()))
				: (UniversalRenderPipelineAsset)Object.Instantiate(QualitySettings.GetRenderPipelineAssetAt(
					(int)settingsData.qualityPreset));

			qualityPreset.upscalingFilter =
				settingsData.fsr ? UpscalingFilterSelection.FSR : UpscalingFilterSelection.Auto;
			qualityPreset.renderScale = settingsData.renderScale;
			QualitySettings.renderPipeline = qualityPreset;

			SetVSync(settingsData);
		}

		private static void ApplyResolution(GraphicsSettingsData settingsData)
		{
			if (UnsupportedPlatforms.IsUnsupportedPlatform) return;
			var windowedOrFullScreen =
				settingsData.windowed ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
			var resolution = settingsData.resolution;
			if (resolution.width != 0 && resolution.height != 0)
			{
				Screen.SetResolution(resolution.width, resolution.height, windowedOrFullScreen, resolution.refreshRate);
			}
		}

		private static void SetVSync(GraphicsSettingsData settingsData)
		{
			if (UnsupportedPlatforms.IsUnsupportedPlatform) return;
			QualitySettings.vSyncCount = settingsData.vsync ? 1 : 0;
		}
	}
}