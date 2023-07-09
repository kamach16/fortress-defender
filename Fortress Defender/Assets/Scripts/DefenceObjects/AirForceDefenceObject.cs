using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForceDefenceObject : MonoBehaviour
{
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private Transform droneSpawnPosition;
    [SerializeField] private float timeBetweenDroneArrival;

    private EnemySpawner enemySpawner;

    private void Awake()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        droneSpawnPosition = GameObject.FindGameObjectWithTag("DroneSpawnPoint").transform;
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenDroneArrival);
            DroneArrival();
        }
    }

    private void DroneArrival()
    {
        if (enemySpawner.currentEnemies.Count != 0)
        {
            GameObject drone = Instantiate(dronePrefab, droneSpawnPosition.position, dronePrefab.transform.rotation);
        }
    }
}
