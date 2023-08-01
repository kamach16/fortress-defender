using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float minTimeBetweenShoots;
    [SerializeField] private float maxTimeBetweenShoots;
    [SerializeField] private float damagePerHit;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            DealDamage();

            float timeBetweenShoots = Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    private void DealDamage()
    {
        int randomEnemyIndex = Random.Range(0, enemySpawner.spawnedEnemiesList.Count);

        if (enemySpawner.spawnedEnemiesList.Count != 0)
        {
            EnemyController targetedEnemy = enemySpawner.spawnedEnemiesList[randomEnemyIndex];

            targetedEnemy.TakeDamage(damagePerHit, targetedEnemy.transform.position + new Vector3(0, 1.5f, 0));

            audioSource.Play();
        }
    }
}
