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
    [SerializeField] public List<EnemyController> currentEnemies = new List<EnemyController>();
    [SerializeField] private int killedEnemies;
    [SerializeField] private int spawnedEnemies;
    [SerializeField] private int enemiesAmountToSpawnPerLevel;

    [Header("Others")]
    [SerializeField] private Transform fortress;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        GameManager.OnNewLevelStarted += RestartSpawner;
    }

    private void OnDisable()
    {
        GameManager.OnNewLevelStarted -= RestartSpawner;
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
        currentEnemies.Add(enemyToAdd);
    }

    public void DeleteEnemy(EnemyController enemyToDelete)
    {
        killedEnemies++;
        currentEnemies.Remove(enemyToDelete);

        if (killedEnemies >= enemiesAmountToSpawnPerLevel) gameManager.Invoke("WinLevel", 2f);
    }

    private void RestartSpawner()
    {
        killedEnemies = 0;
        spawnedEnemies = 0;
    }

    private GameObject GetRandomEnemyType()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Count)];
    }

    private float GetRandomZOffset()
    {
        return Random.Range(minZPosSpawnOffset, maxZPosSpawnOffset);
    }
}
