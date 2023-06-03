using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float flyDistance;
    [SerializeField] private float bottomShootBorder;
    [SerializeField] private float topShootBorder;
    public int manaGainOnRicochhet;
    public int healthGainOnRicochhet;
    public float RicochetChance;
    private float _defaultRicochetChance;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform ShootPos; // откуда стреляем

    private void Start()
    {
        _defaultRicochetChance = RicochetChance;
    }
    public void MakeShoot()
    {
        // если камера повернута по х больше чем bottomShootBorder  меньше чем topShootBorder, то выстрел не производится
        if (camera.transform.rotation.eulerAngles.x > bottomShootBorder && camera.transform.rotation.eulerAngles.x < topShootBorder)
        {
            return;
        }
        Quaternion rotation = Quaternion.Euler(camera.transform.localEulerAngles.x,
                                               transform.localEulerAngles.y,
                                               transform.localEulerAngles.z);

        GameObject _bullet =  Instantiate(bullet, ShootPos.position, rotation);
        _bullet.GetComponent<Bullet>().SetFlyDistance(flyDistance);
    }
    public void ChangeRicochetChance(float chance)
    {
        if (_defaultRicochetChance + chance > 1.0)
        {
            RicochetChance = 1;
        }
        else
        {
            RicochetChance = _defaultRicochetChance + chance;
        }
        Debug.Log($"RicochetChance: {RicochetChance}");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MakeShoot();
        }
    }
}
