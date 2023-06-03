using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedEnemyBehavior : EnemyBehavior
{
    // Main characteristics
    [SerializeField] private float atackDistnace;
    [SerializeField] private float atackOffset;
    [SerializeField] private float secondsTillAtack;
    [SerializeField] private float secondsTillRise;
    [SerializeField] private float topPosition;
    [SerializeField] private float riseSpeed;
    [SerializeField] private float flySpeed;
    [SerializeField] private int _damage;
    private float _tempMovespeed;
    private bool isAtacking = false;
    private bool isGrounded = false;

    [SerializeField] private EnemyHealthControll health;

    private Vector3 rotationVector// направление  передвижения
    {
        get
        {
            return new Vector3(target.position.x - transform.position.x, 0.0f, target.position.z - transform.position.z);
        }
    }
    void Start()
    {
        _score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        rb = GetComponent<Rigidbody>();
        NMAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        _playerHealth = target.gameObject.GetComponent<HealthControll>();
        _playerMana = target.gameObject.GetComponent<ManaControll>();
        _playerShoot = target.gameObject.GetComponent<Shoot>();

        _tempMovespeed = NMAgent.speed;
    }
    void Update()
    {
        ControllDistance();
        Move();
    }
    private void Move()
    {
        Quaternion rotation = Quaternion.LookRotation(rotationVector);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (NMAgent.enabled)
            NMAgent.SetDestination(target.position);
    }
    private void ControllDistance()
    {
        if (isAtacking)
        {
            if (rotationVector.magnitude  > atackDistnace + atackOffset)
            {
                StopAllCoroutines();
                if (isGrounded)
                {
                    NMAgent.enabled = true;
                }
                rb.useGravity = true;
                isAtacking = false;
            }
            return;
        }
        if (rotationVector.magnitude <= atackDistnace)
        {
            StopMooving();
            isAtacking = true;
            StartCoroutine(AtackCoroutine());
        }
        else
        {
            ContinueMooving();
        }
    }
    private void StopMooving()
    {
        NMAgent.speed = 0;
    }
    private void ContinueMooving()
    {
        NMAgent.speed = _tempMovespeed;
    }
    private IEnumerator AtackCoroutine()
    {
        NMAgent.enabled = false;
        rb.useGravity = false;
        yield return new WaitForSeconds(secondsTillRise);
        yield return StartCoroutine(Utils.MoveToTarget(transform, new Vector3(transform.position.x, target.position.y + topPosition, transform.position.z), riseSpeed));
        yield return new WaitForSeconds(secondsTillAtack);
        yield return StartCoroutine(Utils.MoveToTarget(transform, target.position, flySpeed));
        yield return StartCoroutine(Utils.MoveToTarget(transform, new Vector3(transform.position.x, 0.1f, transform.position.z), flySpeed));
        NMAgent.enabled = true;
        rb.useGravity = true;
        isAtacking = false;
    }
    private void DieWithoutBuff()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth.ChangeHealth(-_damage);
            DieWithoutBuff();
            return;
        }
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            PlayerBullet bullet = collision.gameObject.GetComponent<PlayerBullet>();
            int bulletDamage = bullet.GetDamage();

            if (bullet.GetRicochetCount() > 1)
                _buffPlayer = true;
            else
                _buffPlayer = false;

            health.ChangeHealth(-bulletDamage);
            return;
        }
        if (collision.gameObject.tag == ("Ground"))
        {
            NMAgent.enabled = true;
            isGrounded = true;
            return;
        }
        if (isAtacking) 
        {
            StopCoroutine(AtackCoroutine());
            rb.useGravity = true;
            return;
        }


    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded =  false;
        }
    }
}
