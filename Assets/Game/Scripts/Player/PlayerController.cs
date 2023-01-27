using Game.Scripts.Combat;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform visual;
        [SerializeField] private AnimationController animationController;
        [Header("Core Specs")]
        [SerializeField] private float speed = 10f;
        [SerializeField] private float rotatingSpeed = 10f;
        private Shooter shooter;

        private void Start()
        {
            shooter = GetComponent<Shooter>();
        }

        private void Update()
        {
            Move();
        }
        
        private void Move()
        {
            transform.Translate(
                InputManager.Instance.GetJoystickInput() * (speed * Time.deltaTime), 
                Space.Self);
            animationController.SetMovementBlendTreeValue(InputManager.Instance.GetJoystickSpeed());
            // do not change the rotation of visual while character is shooting
            if(shooter.IsShooting) return;
        
            visual.forward = Vector3.Slerp(
                visual.forward,
                InputManager.Instance.GetJoystickInput(),
                Time.deltaTime * rotatingSpeed);
        }
    }
}
