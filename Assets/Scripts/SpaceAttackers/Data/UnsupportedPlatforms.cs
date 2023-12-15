using UnityEngine;

namespace SpaceAttackers.Data
{
	public static class UnsupportedPlatforms
	{
		public static bool IsUnsupportedPlatform => Application.platform is RuntimePlatform.WebGLPlayer
			or RuntimePlatform.LinuxEditor
			or RuntimePlatform.WindowsEditor or RuntimePlatform.OSXEditor;
	}
}