using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForceDefenceObject : MonoBehaviour
{
    [SerializeField] private GameObject dronePrefab;
    [SerializeField] private Transform droneSpawnPosition;
    [SerializeField] private float timeBetweenDroneArrival;

    private void Awake()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        droneSpawnPosition = GameObject.FindGameObjectWithTag("DroneSpawnPoint").transform;
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeBetweenDroneArrival);
        DroneArrival();
    }

    private void DroneArrival()
    {
        GameObject drone = Instantiate(dronePrefab, droneSpawnPosition.position, dronePrefab.transform.rotation);
    }
}
