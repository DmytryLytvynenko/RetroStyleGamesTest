using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected int manaGain;

    //Links
    protected Transform target;
    protected NavMeshAgent NMAgent;
    protected Rigidbody rb;
    protected Score _score;
    protected ManaControll _playerMana;
    protected HealthControll _playerHealth;
    protected Shoot _playerShoot;
    protected bool _buffPlayer = false;

    public void Die()
    {
        if (_buffPlayer)
        {
            GivetBuff();
        }
        _score.IncreaseKillCount();
        _playerMana.ChangeMana(manaGain);
        Destroy(this.gameObject);
    }
    private void GivetBuff()
    {
         GivetManaOrHelth();
    }
    private void GivetManaOrHelth()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            _playerHealth.ChangeHealth(_playerShoot.healthGainOnRicochhet);
            Debug.Log("Health gain");
        }
        else
        {
            _playerMana.ChangeMana(_playerShoot.manaGainOnRicochhet);
            Debug.Log($"Mana gain{_playerShoot.manaGainOnRicochhet}");
        }
    }
}
