using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDefenceObject : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float minTimeBetweenShoots;
    [SerializeField] private float maxTimeBetweenShoots;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserSpawnPositon;
    [SerializeField] private Transform turretModel;
    [SerializeField] private ParticleSystem launchLaserVFX;

    private int randomEnemyIndex;

    private IEnumerator Start()
    {
        while (true)
        {
            LaunchLaser();

            float timeBetweenShoots = Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    private void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (enemySpawner.currentEnemies.Count != 0 && randomEnemyIndex < enemySpawner.currentEnemies.Count)
        {
            EnemyController targetedEnemy = enemySpawner.currentEnemies[randomEnemyIndex];

            Quaternion targetRotation = Quaternion.LookRotation(turretModel.position - (targetedEnemy.transform.position + new Vector3(0, 1.5f, 0)));
            turretModel.rotation = Quaternion.Slerp(turretModel.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void LaunchLaser()
    {
        randomEnemyIndex = Random.Range(0, enemySpawner.currentEnemies.Count);

        if (enemySpawner.currentEnemies.Count != 0)
        {
            GameObject laser = Instantiate(laserPrefab, laserSpawnPositon.position, laserSpawnPositon.rotation);
            launchLaserVFX.Play();
        }
    }
}
