using UnityEngine;

namespace Game.Scripts.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int totalHealth = 100;
        public bool IsAlive { get; private set; }

        private void Start()
        {
            IsAlive = true;
        }

        public void TakeDamage(int amount)
        {
            totalHealth -= amount;
            print(amount);
            if (totalHealth > 0) return;

            Die();
        }

        private void Die()
        {
            IsAlive = false;
        }
    }
}
