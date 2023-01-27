using System;
using Game.Scripts.Combat;
using Game.Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Environment
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private MeshRenderer[] breakableFrames;
        [Header("Specs")]
        [SerializeField] private int healthPointsPerFrame = 33;

        public int InteractionCount { get; set; }
        public bool CanInteract { get; set; }

        private int currentBreakableFrameIndex = 0;

        private void Start()
        {
            GameManager.ActionLevelStart += GameManager_ActionLevelStart;
            GameManager.ActionLevelPassed += GameManager_ActionLevelPassed;
            
            healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
            InteractionCount = healthSystem.TotalHealth;
        }

        

        public void Interact()
        {
            if (!CanInteract) return;
            
            Repair();
        }
        
        private void Repair()
        {
            healthSystem.FillHealth(3);
            
            if (healthSystem.CurrentHealth % healthPointsPerFrame == 0)
            {
                breakableFrames[currentBreakableFrameIndex - 1].enabled = true;
                currentBreakableFrameIndex--;
            }
        }
    
        /// <summary>
        /// e is damage value that taken
        /// </summary>
        private void HealthSystem_OnTakeDamage(object sender, EventArgs eventArgs)
        {
            HandleWithDamage();
        }

        private void HandleWithDamage()
        {
            int frameRemoveCount = healthSystem.GetTotalDamage() / healthPointsPerFrame;
            
            if (currentBreakableFrameIndex >= frameRemoveCount) return;

            frameRemoveCount -= currentBreakableFrameIndex;

            int removeTo = Mathf.Min(
                breakableFrames.Length, 
                currentBreakableFrameIndex + frameRemoveCount);
            
            for (int i = currentBreakableFrameIndex; i < removeTo; i++)
            {
                var breakableFrame = breakableFrames[i];
                breakableFrame.enabled = false;
            }

            currentBreakableFrameIndex += frameRemoveCount;
        }
        
        private void GameManager_ActionLevelPassed()
        {
            CanInteract = true;
        }

        private void GameManager_ActionLevelStart()
        {
            CanInteract = false;
        }

        private void OnDestroy()
        {
            GameManager.ActionLevelStart -= GameManager_ActionLevelStart;
            GameManager.ActionLevelPassed -= GameManager_ActionLevelPassed;
            healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
        }
    }
}
