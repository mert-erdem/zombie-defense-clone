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
        public int TotalHealth => totalHealth;
        public int CurrentHealth { get; private set; }

        private void Start()
        {
            IsAlive = true;
            CurrentHealth = totalHealth;
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            OnTakeDamage?.Invoke(this, amount);
            
            if (CurrentHealth > 0) return;

            Die();
        }

        public void FillHealth(int amount)
        {
            CurrentHealth = Mathf.Min(totalHealth, CurrentHealth + amount);
        }
        
        private void Die()
        {
            IsAlive = false;
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }
}
