using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //окружность на которой спавнятся враги: круг с центром spawnAreaCenter и радиусом spawnAreaRadius
    [SerializeField] private Transform spawnAreaCenter;// центр окружности на которой спавнятся враги 
    [SerializeField] private float spawnAreaRadius;// радиус окружности на которой спавнятся враги 

    [SerializeField] private float increaseDifficultyCooldown;// время через которое увеличивается скорость спавна или количество заспавненых врагов 
    [SerializeField] private float spawnHeight;// высота на которой спавнится враг
    [SerializeField] private float _bossSpawnChance;
    [SerializeField] private float spawnSpeed;
    private float enemyMultiplier = 2;// время через которое увеличивается скорость спавна или количество заспавненых врагов 
    private float multipleSpawnChance = 0;// время через которое увеличивается скорость спавна или количество заспавненых врагов 


    public GameObject[] enemies;// Кого спавним
    private GameObject enemy;

    private float timer;

    private void Start()
    {
        StartCoroutine(IncreasingDifficultyCoroutine());
    }

    void Update()
    {
        if (timer >= spawnSpeed)
        {
            SpawnOnCircle();
            timer = 0;
        }
        else
            timer += Time.deltaTime;
    }

    private void SpawnOnCircle()
    {
        ChooseEnemy();
        float x = Random.Range(0, spawnAreaRadius);
        float z = Mathf.Sqrt(Mathf.Pow(spawnAreaRadius, 2) - Mathf.Pow(x, 2)); // вычисление второой точки окружности по формуле пифагора

        int minus = Random.Range(0, 2);
        if (minus == 0)
        {
            z *= -1;
        }
        minus = Random.Range(0, 2);
        if (minus == 0)
        {
            x *= -1;
        }

        Vector3 position = new Vector3(x, spawnHeight, z);

        int enemyCount = 1;
        float randNum = Random.Range(0f,1f);
        if (randNum < multipleSpawnChance)
        {
            enemyCount = (int)enemyMultiplier;
        }
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemy, position, Quaternion.identity);
        }

    }
    private void ChooseEnemy()
    {
        int maxRandomNumber = (int)(1 / _bossSpawnChance);
        int enemyNumber = Random.Range(0, maxRandomNumber);
        if (enemyNumber == 0)
            enemy = enemies[0];// Враг под индексом 0 - босс
        else
            enemy = enemies[1];

    }
    private IEnumerator IncreasingDifficultyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseDifficultyCooldown);
            if (spawnSpeed > 2)
            {
                spawnSpeed--;
            }
            else
            {
                if (multipleSpawnChance < 1)
                    multipleSpawnChance += 0.05f;
                else
                    enemyMultiplier += 0.5f;
            }
            Debug.Log($"Spawn Speed: {spawnSpeed}, multipleSpawnChance: {multipleSpawnChance}, enemyMultiplier: {enemyMultiplier}");
        }
    }
}
