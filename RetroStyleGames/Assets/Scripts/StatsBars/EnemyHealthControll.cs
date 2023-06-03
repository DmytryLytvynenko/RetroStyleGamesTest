using System;
using UnityEngine;

public class EnemyHealthControll : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private EnemyBehavior enemy;
    private int currentHealth;

    public event Action<float> HealthChanged;

    public void Start()
    {
        enemy = gameObject.GetComponent<EnemyBehavior>();
        currentHealth = maxHealth;
    }

    private void Update()
    {

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
        HealthChanged?.Invoke(currentHealthAsPercentage);
    }

    private void Die()
    {
        HealthChanged?.Invoke(0);
        enemy.Die();
    }
}
