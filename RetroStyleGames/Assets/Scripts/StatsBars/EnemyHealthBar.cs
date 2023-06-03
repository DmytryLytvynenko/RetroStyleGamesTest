
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFilling;
    [SerializeField] private EnemyHealthControll enemyHealth;
    [SerializeField] private Gradient gradient;

    private Camera camera;
    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        enemyHealth.HealthChanged += OnHealthChanged;
        healthBarFilling.color = gradient.Evaluate(1);
    }
    private void OnDestroy()
    {
        enemyHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercentage)
    {
        healthBarFilling.fillAmount = valueAsPercentage;
        healthBarFilling.color = gradient.Evaluate(valueAsPercentage);
    }
}
