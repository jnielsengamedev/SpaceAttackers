using System;

namespace SpaceAttackers.Data
{
	[Serializable]
	public class GraphicsSettingsData
	{
		public Resolution resolution;
		public bool vsync;
		public bool windowed;
		public QualityPresets qualityPreset = QualityPresets.Default;
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
		public int width;
		public int height;
		public RefreshRate refreshRate;

		public override string ToString()
		{
			return $"{width}x{height} {refreshRate.Value}Hz";
		}

		public bool Equals(Resolution resolution)
		{
			return width == resolution.width && height == resolution.height &&
			       refreshRate.Equals(resolution.refreshRate);
		}

		public static explicit operator Resolution(UnityEngine.Resolution resolution)
		{
			return new Resolution
			{
				width = resolution.width,
				height = resolution.height,
				refreshRate = resolution.refreshRateRatio
			};
		}
	}

	[Serializable]
	public struct RefreshRate
	{
		public uint denominator;
		public uint numerator;
		public double Value => numerator / denominator;

		public bool Equals(RefreshRate refreshRate)
		{
			return denominator == refreshRate.denominator && numerator == refreshRate.numerator;
		}

		public static implicit operator UnityEngine.RefreshRate(RefreshRate refreshRate)
		{
			return new UnityEngine.RefreshRate
			{
				numerator = refreshRate.numerator,
				denominator = refreshRate.numerator
			};
		}

		public static implicit operator RefreshRate(UnityEngine.RefreshRate refreshRate)
		{
			return new RefreshRate
			{
				numerator = refreshRate.numerator,
				denominator = refreshRate.denominator
			};
		}
	}

	public enum QualityPresets
	{
		Low = 0,
		Medium = 1,
		High = 2,
		Default = 3
	}
}