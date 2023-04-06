using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Phases")]
    public bool buildingPhase;
    public float buildingPhaseTimer = 30f;

    [Header("Wave Management")]
    public int currentWave;
    public float timeBetweenSpawns;
    public List<Enemy> enemiesInWave;
    public Enemy basicEnemyPrefab, tankEnemyPrefab, speedEnemyPrefab;

    [Header("Scrap")]
    public int totalScrap = 0;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartBuildPhase());
    }

    public IEnumerator StartBuildPhase()
    {
        buildingPhase = true;

        HUD.instance.PlayPhaseTextAnimation(buildingPhase);

        yield return new WaitForSeconds(buildingPhaseTimer);

        buildingPhase = false;
        currentWave++;
        HUD.instance.PlayPhaseTextAnimation(buildingPhase);

        yield return new WaitForSeconds(2f);

        SpawnWave(2 * currentWave, Mathf.RoundToInt(1.5f * currentWave), 1 * currentWave);
    }

    public IEnumerator SpawnEnemies()
    {
        foreach (Enemy enemy in enemiesInWave)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            Spawner.instance.SpawnEnemy(enemy);
        }

        yield return new WaitUntil(() => FindObjectsOfType<Enemy>().Length <= 0);
        Debug.Log("Wave " + currentWave + " Finished!");
        StartCoroutine(StartBuildPhase());
    }

    public void SpawnWave(int numberOfSpeed, int numberOfBasic, int numberOfTanks)
    {

        for (int i = 0; i < numberOfSpeed; i++)
        {
            enemiesInWave.Add(speedEnemyPrefab);
        }

        for (int i = 0; i < numberOfBasic; i++)
        {
            enemiesInWave.Add(basicEnemyPrefab);
        }

        for (int i = 0; i < numberOfTanks; i++)
        {
            enemiesInWave.Add(tankEnemyPrefab);
        }

        StartCoroutine(SpawnEnemies());
    }

    public void AddScrap()
    {
        totalScrap++;
    }

    public void RemoveScrap(int amount)
    {
        totalScrap -= amount;
    }
}
