using System;
using UnityEngine;

namespace Game.Scripts.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler OnTakeDamage;
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

        public bool TakeDamage(int amount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            OnTakeDamage?.Invoke(this, EventArgs.Empty);

            if (CurrentHealth > 0)
                return false;

            Die();
            
            return true;
        }

        public void FillHealth(int amount)
        {
            IsAlive = true;
            CurrentHealth = Mathf.Min(totalHealth, CurrentHealth + amount);
        }

        public int GetTotalDamage()
        {
            return TotalHealth - CurrentHealth;
        }
        
        private void Die()
        {
            IsAlive = false;
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }
}
