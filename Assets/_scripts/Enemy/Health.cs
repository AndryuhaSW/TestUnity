using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action<Health> Spawned;
    public event Action HealthChanged;
    public event Action Killed;
    public float currentHealth { get; set; }
    public float maxHealth { get; set; }

    public void Initialize(float maxHealth)
    {
        Spawned?.Invoke(this);
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public void PlusHealth(float health)
    {
        if (currentHealth + health >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
        HealthChanged?.Invoke();
    }

    public void MinusHealth(float health)
    {
        if (currentHealth - health <= 0)
        {
            currentHealth = 0;
            Kill();
        }
        else
        {
            currentHealth -= health;
        }
        HealthChanged?.Invoke();
    }

    public void Kill()
    {
        Killed?.Invoke();
    }

    public void Reset()
    {
        currentHealth = maxHealth;
    }
}