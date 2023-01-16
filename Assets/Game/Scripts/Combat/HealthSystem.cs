using System;
using UnityEngine;

namespace Game.Scripts.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler<int> OnTakeDamage;
        public event EventHandler OnDie;
        [SerializeField] private int totalHealth = 100;
        public bool IsAlive { get; private set; }

        private int currentHealth;

        private void Start()
        {
            IsAlive = true;
            currentHealth = totalHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            OnTakeDamage?.Invoke(this, amount);
            
            if (currentHealth > 0) return;

            Die();
        }

        public void FillHealth(int amount)
        {
            currentHealth = Mathf.Min(totalHealth, currentHealth + amount);
        }
        
        private void Die()
        {
            IsAlive = false;
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }
}
