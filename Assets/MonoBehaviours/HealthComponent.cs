using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    
    public HealthData healthData { get; }
    public int currentHealth { get; private set; }
    public int previousHealth { get; private set; }
    public float healthPercentage => currentHealth / healthData.maxHealth;
    public float previousHealthPercentage => previousHealth / healthData.maxHealth;
    public event System.Action OnHealthDepleted;

    private List<Tuple<float, int>> healthChanges;

    private void Start()
    {
        healthChanges = new List<Tuple<float, int>>();
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, healthData.maxHealth);
        if (currentHealth <= 0)
        {
            OnHealthDepleted?.Invoke();
        }

        healthChanges.Add(new Tuple<float, int>(Time.time, currentHealth));
    }

    void Update()
    {
        if (healthChanges.Count > 0)
        {
            var firstChange = healthChanges[0];
            if (Time.time > firstChange.Item1 + 1.5f)
            {
                previousHealth = firstChange.Item2;
                healthChanges.RemoveAt(0);
            }
        }
    }
}