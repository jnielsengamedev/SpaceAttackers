using SpaceAttackers.GameManager;
using UnityEngine.UIElements;

namespace SpaceAttackers.UI.Game.Views
{
	public class HUD : View
	{
		private Label _score;
		private Label _lives;

		public HUD(VisualElement element, BaseController controller) : base(element, controller)
		{
		}

		public override void GetElements()
		{
			_score = MainElement.Q<Label>("Score");
			_lives = MainElement.Q<Label>("Lives");
		}

		public override void RegisterEvents()
		{
			Score.Singleton.ScoreUpdated += SetScoreLabel;
			Lives.Singleton.LivesUpdated += SetLivesLabel;
		}

		public override void UnregisterEvents()
		{
			Score.Singleton.ScoreUpdated -= SetScoreLabel;
			Lives.Singleton.LivesUpdated -= SetLivesLabel;
		}

		private void SetScoreLabel(long score)
		{
			_score.text = $"Score: {score}";
		}

		private void SetLivesLabel(long lives)
		{
			_lives.text = $"Lives: {lives}";
		}
	}
}