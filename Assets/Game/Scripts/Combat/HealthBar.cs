using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Combat
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image imageHealth, imageBackground;
        [SerializeField] private HealthSystem healthSystem;

        private void OnEnable()
        {
            imageHealth.enabled = true;
            imageBackground.enabled = true;
        }

        private void Start()
        {
            healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
            healthSystem.OnFillHealth += HealthSystem_OnFillHealth;
            healthSystem.OnDie += HealthSystem_OnDie;
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }

        private void HealthSystem_OnTakeDamage(object sender, EventArgs e)
        {
            UpdateHealthImage();
        }
    
        private void HealthSystem_OnFillHealth(object sender, EventArgs e)
        {
            UpdateHealthImage();
        }
    
        private void HealthSystem_OnDie(object sender, EventArgs e)
        {
            imageHealth.enabled = false;
            imageBackground.enabled = false;
        }

        private void UpdateHealthImage()
        {
            imageHealth.fillAmount = healthSystem.GetHealthNormalized();
        }

        private void OnDestroy()
        {
            healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
            healthSystem.OnFillHealth -= HealthSystem_OnFillHealth;
            healthSystem.OnDie -= HealthSystem_OnDie;
        }
    }
}
