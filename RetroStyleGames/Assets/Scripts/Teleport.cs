using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _radius;
    private Transform[] _teleportPositions;

    private void Start()
    {
        _teleportPositions = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _teleportPositions[i] = transform.GetChild(i);
        }
    }
    private void TeleportPlayer()
    {
        SetPlayerPositionToAllBullets();

        int index = 0;
        float distance = (_teleportPositions[0].position - _player.position).magnitude;
        for (int i = 1; i < transform.childCount; i++)
        {
            float newDistance = (_teleportPositions[i].position - _player.position).magnitude;
            if (newDistance > distance)
            {
                distance = newDistance;
                index = i;
            }
        }
        _player.position = _teleportPositions[index].position;
    }
    private void SetPlayerPositionToAllBullets()
    {
        LayerMask mask = LayerMask.GetMask("EnemyBullet");
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius, mask);

        foreach (var bullet in overlappedColliders)
        {
            bullet.GetComponent<EnemyBullet>().SetTarget(_player.position);
        }
    } 
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            TeleportPlayer();
        }
    }
}
