using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject pickableCube;
    public GameObject[] walls;
    public int maxCubesCount;// кол-во pickableCube`ов на одной платформе
    public float[] cubePositions;
    public float offset;// докуда подниматься
    public float velocity;// скорость подъема
    public Vector3 wallPos;// позиция стены
    private Vector3 end;


    void Start()
    {
        int cubesCount = Random.Range(1, maxCubesCount);
        int wallNumber = Random.Range(0, walls.Length);
        GameObject wall = Instantiate(walls[wallNumber], transform);
        wall.transform.localPosition = wallPos;
        for (int i = 0; i < cubesCount; i++)
        {
            float posX = Random.Range(-0.4f, 0.4f);
            Vector3 cubePos = new Vector3(posX, 1, cubePositions[i]);
            GameObject cube = Instantiate(pickableCube, transform);
            cube.transform.localPosition = cubePos;
        }

        end = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, end, velocity * Time.deltaTime);
    }
}
