using System;
using UnityEngine;
using UnityEngine.UI;

public class ManaControll : MonoBehaviour
{
    //!!! В будущем нужно исправть дубляж кода
    [SerializeField] private int maxMana = 100;
    [SerializeField] private Button ultimateButton;
    private int currentMana;

    public event Action<float> ManaChanged;
    public int MaxMana { get { return maxMana; } }
    public void Start()
    {
        currentMana = 140;
        ChangeMana(0);
    }

    private void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeMana(-10);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeMana(+10);
        }*/
    }

    public void ChangeMana(int value)
    {
        if (currentMana + value <= 0)
            currentMana = 0;
        else if(currentMana + value >= maxMana)
            currentMana = maxMana;
        else
            currentMana += value;


        if (currentMana == maxMana)
            ultimateButton.interactable = true;
        else
            ultimateButton.interactable = false;


        float currentManaAsPercentage = (float)currentMana / maxMana;
        ManaChanged?.Invoke(currentManaAsPercentage);
    }
    public bool CheckManaCount()
    {
        if (currentMana == maxMana)
            return true;
        else
            return false;
    }
}
