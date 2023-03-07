using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float timeBetweenSpawns;
    bool isSpawning;

    void Update()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeBetweenSpawns);
        Instantiate(enemy, transform.position, Quaternion.identity);
        isSpawning = false;
    }
}
