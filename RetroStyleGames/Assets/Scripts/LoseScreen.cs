using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LoseScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textOnMainScreen;
    [SerializeField] private TextMeshProUGUI _textOnLoseScreen;

    void Start()
    {
        _textOnLoseScreen.text = _textOnMainScreen.text;
        _textOnMainScreen.gameObject.SetActive(false);
    }
}
