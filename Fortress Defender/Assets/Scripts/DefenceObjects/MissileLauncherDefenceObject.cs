using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherDefenceObject : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform missileSpawnPositon;
    [SerializeField] private Transform turretModel;
    [SerializeField] private ParticleSystem launchMissileVFX;

    private int randomEnemyIndex;

    private IEnumerator Start()
    {
        while (true)
        {
            LaunchMissile();
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

    private void LaunchMissile()
    {
        randomEnemyIndex = Random.Range(0, enemySpawner.currentEnemies.Count);

        if (enemySpawner.currentEnemies.Count != 0)
        {
            GameObject missile = Instantiate(missilePrefab, missileSpawnPositon.position, missileSpawnPositon.rotation);
            launchMissileVFX.Play();
        }
    }
}
