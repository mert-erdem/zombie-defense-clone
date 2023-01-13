using Game.Scripts.Combat;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform visual;
        [Header("Core Specs")]
        [SerializeField] private float speed = 10f;
        [SerializeField] private float rotatingSpeed = 10f;
        private Vector3 inputDir;
        private Shooter shooter;

        private void Start()
        {
            shooter = GetComponent<Shooter>();
        }

        private void Update()
        {
#if UNITY_EDITOR
            GetKeyboardInput();
            // joystick input will be added
#endif
            Move();
        }

    
        private void Move()
        {
            transform.Translate(
                inputDir * (speed * Time.deltaTime), 
                Space.Self);
            // do not change the rotation of visual while character is shooting
            if(shooter.IsShooting) return;
        
            visual.forward = Vector3.Slerp(
                visual.forward,
                inputDir,
                Time.deltaTime * rotatingSpeed);
        }

        private void GetKeyboardInput()
        {
            inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
    }
}
