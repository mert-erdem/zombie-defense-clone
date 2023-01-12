using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        print(totalHealth + " " + amount);
        
        if (totalHealth > 0) return;

        Die();
    }

    private void Die()
    {
        IsAlive = false;
    }
}
