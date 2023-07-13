using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForceDefenceObject : MonoBehaviour
{
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private Transform droneSpawnPosition;
    [SerializeField] private float timeBetweenDroneArrival;
    [SerializeField] private bool isActive = true;

    private EnemySpawner enemySpawner;

    private void OnEnable()
    {
        GameManager.OnDefeat += DeactiveDefenceObject;
    }

    private void OnDisable()
    {
        GameManager.OnDefeat -= DeactiveDefenceObject;
    }

    private void Awake()
    {
        SetVariables();
    }

    private void DeactiveDefenceObject()
    {
        isActive = false;
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
        if (!isActive) return;

        if (enemySpawner.spawnedEnemiesList.Count != 0)
        {
            GameObject drone = Instantiate(dronePrefab, droneSpawnPosition.position, dronePrefab.transform.rotation);
        }
    }
}
