using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] protected int _manaDamage;
    protected override void Start()
    {
        base.Start();
        _target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (_target == null)
        {
            Die();
            return;
        }
        Move();
    }

    public int GetManaDamage()
    {
        return _manaDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))// При столкновении с другой вражеской пулей эта не  уничтожается
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            ManaControll player = collision.gameObject.GetComponent<ManaControll>();

            player.ChangeMana(-_manaDamage);
        }
        Die();
    }
}
