using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : CoreComponent
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    //Create an Event which tracks when the Health reaches zero
    public event Action HealthZero;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }
    public float GetCurrentHealth()
    {
        //Debug.Log("Current Health:"+ currentHealth);
        return currentHealth;
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Invoke the event.
            HealthZero?.Invoke();
            base.Log("Health is zero!");
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public float getHealth()
    {
        return currentHealth;
    }
}
