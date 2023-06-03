using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{

    [SerializeField] private GameObject buttonName;
    [SerializeField] private TextMeshProUGUI coolDownText;

    [SerializeField] private float coolDownTime;
    private float tempCoolDownTime;
    private bool canCount;

    void Start()
    {
        tempCoolDownTime = coolDownTime;
    }
    void Update()
    {
        if (!canCount)
        {
            return;
        }
        else
        {
            coolDownTime -= Time.deltaTime;
            coolDownText.text = Math.Round(coolDownTime, 1).ToString();
            if (coolDownTime <= 0)
            {
                coolDownTime = tempCoolDownTime;
                gameObject.GetComponent<Button>().interactable = true;
                canCount = false;
                buttonName.SetActive(true);
                coolDownText.gameObject.SetActive(false);
            }
        }

    }
    public void StartCooldown()
    {
        gameObject.GetComponent<Button>().interactable = false;
        canCount = true;
        buttonName.SetActive(false);
        coolDownText.gameObject.SetActive(true);
    }
}
