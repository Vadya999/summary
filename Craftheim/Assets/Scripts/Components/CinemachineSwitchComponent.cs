using UnityEngine;

namespace Source.Scripts.Components
{
    public class CinemachineSwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private bool followCamera;
        
        public void Switch()
        {
            if (followCamera)
            {
                animator.Play("Follow");
            }
            else
            {
                animator.Play("Scoreboard");
            }

            followCamera = !followCamera;
        }
    }
}