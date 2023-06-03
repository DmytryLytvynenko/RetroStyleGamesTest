using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFilling;
    [SerializeField] private HealthControll Health;
    [SerializeField] private Gradient gradient;


    private void Awake()
    {
        Health.HealthChanged += OnHealthChanged;
        healthBarFilling.color = gradient.Evaluate(1);
    }
    private void OnDestroy()
    {
        Health.HealthChanged -= OnHealthChanged;
    }


    private void OnHealthChanged(float valueAsPercentage)
    {
        healthBarFilling.fillAmount = valueAsPercentage;
        healthBarFilling.color = gradient.Evaluate(valueAsPercentage);
    }
}
