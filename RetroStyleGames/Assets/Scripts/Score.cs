using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _text;
    private int _killCount = 0;

    private void Start()
    {
        CahngeScore();
    }
    public void CahngeScore()
    {
        _text.text = $"Score: {_killCount}";
    }
    public void IncreaseKillCount()
    {
        _killCount++;
        CahngeScore();
    }
}
