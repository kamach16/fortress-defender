using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private float minZPosSpawnOffset;
    [SerializeField] private float maxZPosSpawnOffset;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Transform fortress;

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
        GameObject spawnedEnemy = Instantiate(GetRandomEnemy(), enemySpawnPoint.position + new Vector3(0, 0, GetRandomZOffset()), Quaternion.Euler(0, 90, 0));
        spawnedEnemy.GetComponent<EnemyController>().SetFortress(fortress);
    }

    private GameObject GetRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    private float GetRandomZOffset()
    {
        return Random.Range(minZPosSpawnOffset, maxZPosSpawnOffset);
    }
}
