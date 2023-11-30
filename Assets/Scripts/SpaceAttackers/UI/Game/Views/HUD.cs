using SpaceAttackers.GameManager;
using UnityEngine.UIElements;

namespace SpaceAttackers.UI.Game.Views
{
	public class HUD : View
	{
		private Label _score;

		public HUD(VisualElement element, BaseController controller) : base(element, controller)
		{
		}

		public override void GetElements()
		{
			_score = MainElement.Q<Label>("Score");
		}

		public override void RegisterEvents()
		{
			Score.Singleton.ScoreUpdated += SetScoreLabel;
		}

		public override void UnregisterEvents()
		{
			Score.Singleton.ScoreUpdated -= SetScoreLabel;
		}

		private void SetScoreLabel(long score)
		{
			_score.text = $"Score: {score}";
		}
	}
}