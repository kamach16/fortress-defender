using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDefenceObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenMissileLaunch;
    [SerializeField] private float timeToStartLaunchingMissiles;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform missileSpawnPosition;
    [SerializeField] private bool startedLaunchingMissiles;

    private EnemySpawner enemySpawner;

    private void Awake()
    {
        Destroy(gameObject, 15);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (!startedLaunchingMissiles)
            {
                yield return new WaitForSeconds(timeToStartLaunchingMissiles);
                startedLaunchingMissiles = true;
            }

            LaunchSingleMissile();
            yield return new WaitForSeconds(timeBetweenMissileLaunch);
        }
    }

    private void Update()
    {
        Move();
    }

    public void SetEnemySpawner(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
    }

    private void LaunchSingleMissile()
    {
        if (enemySpawner.currentEnemies.Count != 0)
        {
            GameObject missile = Instantiate(missilePrefab, missileSpawnPosition.position, missilePrefab.transform.rotation);
            Destroy(missile, 3);
        }
    }

    private void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
