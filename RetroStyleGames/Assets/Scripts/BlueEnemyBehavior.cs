using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueEnemyBehavior : EnemyBehavior
{
    // Main characteristics
    [SerializeField] private float shootDistnace;
    [SerializeField] private float shootCooldown;

    private bool isShooting = false;
    private bool isGrounded = false;
    private float tempMoveSpeed;
    public int damage;

    //links
    [SerializeField] private EnemyHealthControll health;
    private Transform _shootPos; // откуда стреляем
    private GameObject bullet;

    private Vector3 rotationVector// направление  передвижения
    {
        get
        {
            return new Vector3(target.position.x - transform.position.x, 0.0f, target.position.z - transform.position.z).normalized;
        }
    }
    void Start()
    {
        _score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        bullet = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        target = GameObject.Find("Player").transform;
        _shootPos = this.gameObject.transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        NMAgent = GetComponent<NavMeshAgent>();
        _playerMana = target.gameObject.GetComponent<ManaControll>();
        _playerHealth = target.gameObject.GetComponent<HealthControll>();
        _playerShoot = target.gameObject.GetComponent<Shoot>();

        tempMoveSpeed = NMAgent.speed;
    }
    void Update()
    {
        ControllDistance();
        Move();
    }
    private void Move()
    {
        if (!isGrounded)
        {
            return;
        }
        Quaternion rotation = Quaternion.LookRotation(rotationVector);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        NMAgent.SetDestination(target.position);
    }
    private void ControllDistance()
    {
        if (!isGrounded)
        {
            return;
        }
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        Ray ray = new Ray(origin, 20 * rotationVector);
        Debug.DrawRay(origin, 20 * rotationVector, Color.red);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider == null)
        {
            return;
        }
        else if (!hit.collider.CompareTag("Player"))
        {
            return;
        }
        if (hit.distance <= shootDistnace)
        {
            if (isShooting)
            {
                return;
            }
            StartCoroutine(ShootingCoroutine());
        }
    }
    private void StopMooving()
    {
        NMAgent.speed = 0;
    }
    private void ContinueMooving()
    {
        NMAgent.speed = tempMoveSpeed;
    }
    private IEnumerator ShootingCoroutine()
    {
        isShooting = true;
        StopMooving();

        yield return new WaitForSeconds(shootCooldown);
        Instantiate(bullet, _shootPos.position, Quaternion.identity);

        isShooting = false;
        ContinueMooving();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            PlayerBullet bullet = collision.gameObject.GetComponent<PlayerBullet>();
            int bulletDamage =bullet.GetDamage();

            if (bullet.GetRicochetCount() > 1)
                _buffPlayer = true;
            else
                _buffPlayer = false;

            health.ChangeHealth(-bulletDamage);
        }
        if (collision.gameObject.tag == ("Ground"))
        {
            NMAgent.enabled = true;
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = false;
        }
    }
}
