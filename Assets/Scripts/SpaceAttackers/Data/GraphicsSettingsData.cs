using System;

namespace SpaceAttackers.Data
{
	[Serializable]
	public struct GraphicsSettingsData
	{
		public Resolution resolution;
		public bool vsync;
		public bool windowed;
		public QualityPresets qualityPreset;
		public bool fsr;
		public float renderScale;
	}
	
	[Serializable]
	public struct Resolution
	{
		public long width;
		public long height;
		public long refreshRate;
	}

	public enum QualityPresets
	{
		Low = 0,
		Medium = 1,
		Height = 2
	}
}