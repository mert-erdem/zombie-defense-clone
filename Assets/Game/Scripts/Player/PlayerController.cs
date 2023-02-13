using System;
using Game.Scripts.Combat;
using Game.Scripts.Core;
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
        private HealthSystem healthSystem;

        private bool isAlive = true;

        private void Awake()
        {
            shooter = GetComponent<Shooter>();
            healthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            healthSystem.OnDie += HealthSystem_OnDie;
        }

        private void Update()
        {
            if (!isAlive) return;
            
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
        
        private void HealthSystem_OnDie(object sender, EventArgs e)
        {
            GameManager.ActionGameOver?.Invoke();
            isAlive = false;
            animationController.PerformDieAnim();
        }

        private void OnDestroy()
        {
            healthSystem.OnDie -= HealthSystem_OnDie;
        }
    }
}