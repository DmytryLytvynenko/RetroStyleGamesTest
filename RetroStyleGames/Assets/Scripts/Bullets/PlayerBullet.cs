using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Shoot _playerShoot;
    [SerializeField] private float _searchEnemyRadius;
    [SerializeField] private int _damage;
    private bool _canRicochet = false;
    private float _ricochetChance;
    private int _ricochetCount = 0;

    protected override void Start()
    {
        base.Start();
        _playerShoot = GameObject.Find("Player").GetComponent<Shoot>();

        _ricochetChance = _playerShoot.RicochetChance;

        float chance = UnityEngine.Random.Range(0,1.0f);
        if (chance <= _ricochetChance)
            _canRicochet = true;
    }
    private void Update()
    {
        if (_ricochetCount > 0 && _target == null)
        {
            Die();
            return;
        }
        Move();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 6)
        {
            Die();
            return;
        }
        _ricochetCount += 1;
        if (_canRicochet)
        {
            int index = 0;
            List<Collider> overlappedColliders = FindNearestEnemies(ref index, collision.collider);
            if (overlappedColliders.Count == 0)
                return;
            else
            {
                SetTarget(overlappedColliders[index].transform);
            }

            _canRicochet = false;
        }
        else
        {
            Die();
        }
    }

    public int GetDamage()
    {
        return _damage;
    }
    public int GetRicochetCount()
    {
        return _ricochetCount;
    }
    private List<Collider> FindNearestEnemies(ref int index, Collider currentCollider)
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _searchEnemyRadius, mask);
        List<Collider> overlappedCollidersList = new List<Collider>();

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            if (overlappedColliders[i].name != currentCollider.name)
            {
                overlappedCollidersList.Add(overlappedColliders[i]);
            }
        }

        if (overlappedColliders == null)
            return overlappedCollidersList;


        // берем последний коллайдер в списке
        // он ближайший к пуле после того, с которым она столкнулась
        index = overlappedCollidersList.Count - 1;
        return overlappedCollidersList;
    }
}
