using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherDefenceObject : MonoBehaviour
{
    [SerializeField] private float minTimeBetweenShoots;
    [SerializeField] private float maxTimeBetweenShoots;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isActive = true;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform missileSpawnPositon;
    [SerializeField] private Transform turretModel;
    [SerializeField] private ParticleSystem launchMissileVFX;
    [SerializeField] private AudioSource audioSource;

    private EnemySpawner enemySpawner;

    private int randomEnemyIndex;

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
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            LaunchMissile();

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
        if (!isActive) return;

        if (enemySpawner.spawnedEnemiesList.Count != 0 && randomEnemyIndex < enemySpawner.spawnedEnemiesList.Count)
        {
            EnemyController targetedEnemy = enemySpawner.spawnedEnemiesList[randomEnemyIndex];

            if (targetedEnemy == null) return;

            Quaternion targetRotation = Quaternion.LookRotation(turretModel.position - (targetedEnemy.transform.position + new Vector3(0, 1.5f, 0)));
            turretModel.rotation = Quaternion.Slerp(turretModel.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void LaunchMissile()
    {
        if (!isActive) return;

        randomEnemyIndex = Random.Range(0, enemySpawner.spawnedEnemiesList.Count);

        if (enemySpawner.spawnedEnemiesList.Count != 0)
        {
            GameObject missile = Instantiate(missilePrefab, missileSpawnPositon.position, missileSpawnPositon.rotation);
            launchMissileVFX.Play();
            audioSource.Play();
            Destroy(missile, 5);
        }
    }
}
