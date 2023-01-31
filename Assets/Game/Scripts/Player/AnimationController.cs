using UnityEngine;

namespace Game.Scripts.Player
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetMovementBlendTreeValue(float value)
        {
            animator.SetFloat("Speed", value);
        }
    }
}
