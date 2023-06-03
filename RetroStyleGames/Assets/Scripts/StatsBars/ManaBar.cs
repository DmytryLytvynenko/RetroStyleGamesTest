using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    //!!! В будущем нужно исправть дубляж кода
    [SerializeField] private Image _manaBarFilling;
    [SerializeField] private ManaControll Mana;


    private void Awake()
    {
        Mana.ManaChanged += OnManaChanged;
    }
    private void OnDestroy()
    {
        Mana.ManaChanged -= OnManaChanged;
    }


    private void OnManaChanged(float valueAsPercentage)
    {
        _manaBarFilling.fillAmount = valueAsPercentage;
    }
}
