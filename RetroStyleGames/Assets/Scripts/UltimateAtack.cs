using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateAtack : MonoBehaviour
{
    [SerializeField] private ManaControll _mana;
    [SerializeField] private Score _score;
    [SerializeField] private Vector3 _areaCenter;
    [SerializeField] private float _radius;
    public void UltAtack()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] overlappedColliders = Physics.OverlapSphere(_areaCenter, _radius, mask);

        foreach (var enemy in overlappedColliders)
        {
            Destroy(enemy.gameObject);
            _score.IncreaseKillCount();
        }
        _mana.ChangeMana(-_mana.MaxMana);
    }
}
