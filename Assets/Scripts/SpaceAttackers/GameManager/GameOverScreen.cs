using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceAttackers.GameManager
{
	public class GameOverScreen : MonoBehaviour
	{
		private Animator _animator;
		private bool _acceptInput;
		[SerializeField] private LoadingScreen loadingScreen;
		[SerializeField] private TextMeshProUGUI scoreLabel;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void Start()
		{
			InputManager.Singleton.ShootingFired += Continue;
		}

		public void ShowGameOverScreen()
		{
			scoreLabel.text = $"Score:\n{Score.GameScore}";
			_animator.SetTrigger("ShowGameOverScreen");
		}

		public void AcceptInput()
		{
			_acceptInput = true;
		}

		private void Continue()
		{
			if (!_acceptInput) return;
			_animator.SetTrigger("HideGameOverScreen");
		}

		public void HideGameOverFinished()
		{
			loadingScreen.ShowLoadingScreen(LoadMainMenu);
		}

		private static void LoadMainMenu()
		{
			SceneManager.LoadSceneAsync("MainMenu");
		}
	}
}