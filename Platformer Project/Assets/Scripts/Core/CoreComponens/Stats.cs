using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Health = 0");
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);  
    }
}
