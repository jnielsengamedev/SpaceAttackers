using UnityEngine;

namespace SpaceAttackers.GameManager
{
    public class GameOverScreen : MonoBehaviour
    {
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ShowGameOverScreen()
        {
            _animator.SetTrigger("ShowGameOverScreen");
        }
    }
}
