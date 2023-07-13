using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGunDefenceObject : MonoBehaviour
{
    [SerializeField] private float minTimeBetweenShoots;
    [SerializeField] private float maxTimeBetweenShoots;
    [SerializeField] private float damagePerHit;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isActive = true;
    [SerializeField] private ParticleSystem gunShotVFX;

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
            DealDamage();

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

            Quaternion targetRotation = Quaternion.LookRotation(transform.position - targetedEnemy.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void DealDamage()
    {
        if (!isActive) return;

        randomEnemyIndex = Random.Range(0, enemySpawner.spawnedEnemiesList.Count);

        if (enemySpawner.spawnedEnemiesList.Count != 0)
        {
            EnemyController targetedEnemy = enemySpawner.spawnedEnemiesList[randomEnemyIndex];

            targetedEnemy.TakeDamage(damagePerHit, targetedEnemy.transform.position + new Vector3(0, 1.5f, 0));
            gunShotVFX.Play();
        }
    }
}
