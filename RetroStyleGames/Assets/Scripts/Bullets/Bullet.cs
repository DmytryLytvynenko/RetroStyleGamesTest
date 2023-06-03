using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _timeToLive;
    protected bool straightFly = false;
    protected Rigidbody rb;
    protected Vector3 straightFlyVrctor;
    protected virtual void Start()
    {
        Invoke("Die", _timeToLive);
    }
    protected Vector3 moveVector// направление  передвижения
    {
        get
        {
            return new Vector3(_target.position.x - transform.position.x,
                               _target.position.y - transform.position.y,
                               _target.position.z - transform.position.z).normalized;
        }
    }
    protected void Move()
    { 
        if (straightFly)
        {
            transform.Translate(straightFlyVrctor * _moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
        }
    }
    public void SetTarget(Transform target)
    {
        straightFly = false;
        this._target = target;
    }
    public void SetTarget(Vector3 target)
    {
        straightFly = true;
        Vector3 direction = new Vector3(target.x - transform.position.x,
                                          target.y - transform.position.y,
                                          target.z - transform.position.z).normalized;
        Debug.Log($"{target.x},{target.y},{target.z}");
        straightFlyVrctor = direction;
    }
    public void SetFlyDistance(float distance)
    {
        straightFly = true;
        Vector3 destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
        straightFlyVrctor = (destination - transform.position).normalized;
    }
    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
