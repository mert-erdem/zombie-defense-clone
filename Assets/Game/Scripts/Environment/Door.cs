using Game.Scripts.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Environment
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private MeshRenderer[] breakableFrames;
        [Header("Specs")]
        [SerializeField] private int healthPointsPerFrame = 25;

        public bool CanInteract { get; set; }

        private int currentBreakableFrameIndex = 0;

        private void Start()
        {
            healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
        }
        
        public void Interact()
        {
            Repair();
        }

        private void Repair()
        {
            healthSystem.FillHealth(healthPointsPerFrame);
            breakableFrames[currentBreakableFrameIndex - 1].enabled = true;
            currentBreakableFrameIndex--;
        }
    
        /// <summary>
        /// e is damage value that taken
        /// </summary>
        private void HealthSystem_OnTakeDamage(object sender, int e)
        {
            HandleWithDamage(e);
        }

        private void HandleWithDamage(int damage)
        {
            int frameRemoveCount = damage / healthPointsPerFrame;
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

        private void OnDestroy()
        {
            healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
        }
    }
}
