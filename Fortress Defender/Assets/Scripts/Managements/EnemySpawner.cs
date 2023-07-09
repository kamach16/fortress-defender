using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("About spawner")]
    [SerializeField] private List<GameObject> remainingEnemyTypes = new List<GameObject>();
    [SerializeField] private List<GameObject> currentEnemyTypesToSpawn = new List<GameObject>();
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private float minZPosSpawnOffset;
    [SerializeField] private float maxZPosSpawnOffset;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] public List<EnemyController> spawnedEnemiesList = new List<EnemyController>();
    [SerializeField] private int killedEnemies;
    [SerializeField] private int spawnedEnemies;
    [SerializeField] private int enemiesAmountToSpawnPerLevel;
    [SerializeField] private int waveNumber;

    [Header("Others")]
    [SerializeField] private Transform fortress;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        GameManager.OnNewLevelStarted += RestartSpawner;
        GameManager.OnNewLevelStarted += BuffSpawner;
    }

    private void OnDisable()
    {
        GameManager.OnNewLevelStarted -= RestartSpawner;
        GameManager.OnNewLevelStarted -= BuffSpawner;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (gameManager.lost || spawnedEnemies >= enemiesAmountToSpawnPerLevel) return;

        GameObject spawnedEnemy = Instantiate(GetRandomEnemyType(), enemySpawnPoint.position + new Vector3(0, 0, GetRandomZOffset()), Quaternion.Euler(0, 90, 0));

        EnemyController spawnedEnemyController = spawnedEnemy.GetComponent<EnemyController>();
        spawnedEnemyController.SetFortress(fortress);
        spawnedEnemyController.SetPlayerHealth(playerHealth);
        spawnedEnemyController.SetEnemySpawner(this);
        spawnedEnemyController.SetGameManager(gameManager);
        AddEnemyToCurrentEnemiesList(spawnedEnemyController);

        spawnedEnemies++;
    }

    private void AddEnemyToCurrentEnemiesList(EnemyController enemyToAdd)
    {
        spawnedEnemiesList.Add(enemyToAdd);
    }

    public void DeleteEnemy(EnemyController enemyToDelete)
    {
        killedEnemies++;
        spawnedEnemiesList.Remove(enemyToDelete);

        if (killedEnemies >= enemiesAmountToSpawnPerLevel) gameManager.Invoke("WinLevel", 2f);
    }

    private void RestartSpawner()
    {
        killedEnemies = 0;
        spawnedEnemies = 0;
    }

    private void BuffSpawner()
    {
        enemiesAmountToSpawnPerLevel++;
        waveNumber++;
        timeBetweenSpawns = Mathf.Max(timeBetweenSpawns - 0.1f, 1);

        if (waveNumber % 3 == 0) // run this condition every 3 waves
        {
            GameObject newEnemyType = remainingEnemyTypes[0];
            currentEnemyTypesToSpawn.Add(newEnemyType);

            remainingEnemyTypes.RemoveAt(0);
        }
    }

    private GameObject GetRandomEnemyType()
    {
        return currentEnemyTypesToSpawn[Random.Range(0, currentEnemyTypesToSpawn.Count)];
    }

    private float GetRandomZOffset()
    {
        return Random.Range(minZPosSpawnOffset, maxZPosSpawnOffset);
    }
}
