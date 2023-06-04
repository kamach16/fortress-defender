using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("About spawner")]
    [SerializeField] private List<GameObject> enemyTypes = new List<GameObject>();
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private float minZPosSpawnOffset;
    [SerializeField] private float maxZPosSpawnOffset;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private List<EnemyController> currentEnemies = new List<EnemyController>();

    [Header("Others")]
    [SerializeField] private Transform fortress;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameManager gameManager; 

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnEnemy();
        }
    }

    public List<EnemyController> GetCurrentEnemiesList()
    {
        return currentEnemies;
    }

    private void SpawnEnemy()
    {
        if (gameManager.GetIfLost()) return;

        GameObject spawnedEnemy = Instantiate(GetRandomEnemy(), enemySpawnPoint.position + new Vector3(0, 0, GetRandomZOffset()), Quaternion.Euler(0, 90, 0));

        EnemyController spawnedEnemyController = spawnedEnemy.GetComponent<EnemyController>();
        spawnedEnemyController.SetFortress(fortress);
        spawnedEnemyController.SetPlayerHealth(playerHealth);
        spawnedEnemyController.SetEnemySpawner(this);
        AddEnemyToCurrentEnemiesList(spawnedEnemyController);
    }

    private void AddEnemyToCurrentEnemiesList(EnemyController enemyToAdd)
    {
        currentEnemies.Add(enemyToAdd);
    }

    public void DeleteEnemyFromCurrentEnemiesList(EnemyController enemyToDelete)
    {
        currentEnemies.Remove(enemyToDelete);
    }

    private GameObject GetRandomEnemy()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Count)];
    }

    private float GetRandomZOffset()
    {
        return Random.Range(minZPosSpawnOffset, maxZPosSpawnOffset);
    }
}
