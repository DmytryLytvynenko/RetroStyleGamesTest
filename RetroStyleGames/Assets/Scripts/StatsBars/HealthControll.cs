using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControll : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private Shoot _playerShoot;

    public event Action<float> HealthChanged;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeHealth(-10);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeHealth(+10);
        }
    }

    public void ChangeHealth(int value)
    {
        currentHealth += value;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        float currentHealthAsPercentage = (float)currentHealth / maxHealth;
        _playerShoot.ChangeRicochetChance(1.0f - currentHealthAsPercentage);
        HealthChanged?.Invoke(currentHealthAsPercentage);
    }
/*    private float CalcRicochetChance(float currentHealthAsPercentage)
    {
        return (1.0f - currentHealthAsPercentage);
    }*/

    private void Die()
    {
        HealthChanged?.Invoke(0);
        Time.timeScale = 0;
        LoseScreen.SetActive(true);
    }
}
