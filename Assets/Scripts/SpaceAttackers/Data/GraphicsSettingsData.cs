using System;

namespace SpaceAttackers.Data
{
	[Serializable]
	public class GraphicsSettingsData
	{
		public Resolution resolution;
		public bool vsync;
		public bool windowed;
		public QualityPresets qualityPreset = QualityPresets.High;
		public bool fsr;
		public float renderScale = 1.0f;

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			var data = (GraphicsSettingsData)obj;
			return data.resolution.Equals(resolution) && data.vsync == vsync && data.windowed == windowed &&
			       data.qualityPreset == qualityPreset && data.fsr == fsr && data.renderScale == renderScale;
		}

		public GraphicsSettingsData ShallowClone()
		{
			return (GraphicsSettingsData)MemberwiseClone();
		}
	}

	[Serializable]
	public struct Resolution
	{
		public long width;
		public long height;
		public double refreshRate;

		public override string ToString()
		{
			return $"{width}x{height} {refreshRate}Hz";
		}

		public bool Equals(Resolution resolution)
		{
			return width == resolution.width && height == resolution.height && refreshRate == resolution.refreshRate;
		}
	}

	public enum QualityPresets
	{
		Low = 0,
		Medium = 1,
		High = 2
	}
}