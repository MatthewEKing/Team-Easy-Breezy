using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    //[SerializeField] GameObject enemy;
    [SerializeField] float timeBetweenSpawns;
    bool isSpawning;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isSpawning)
        {
            //StartCoroutine(SpawnEnemy());
        }
    }

    public void SpawnEnemy(Enemy enemy)
    {
        Instantiate(enemy.gameObject, transform.position, Quaternion.identity);
    }
}
