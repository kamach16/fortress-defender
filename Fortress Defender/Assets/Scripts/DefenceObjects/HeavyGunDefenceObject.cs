using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGunDefenceObject : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float minTimeBetweenShoots;
    [SerializeField] private float maxTimeBetweenShoots;
    [SerializeField] private float damagePerHit;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ParticleSystem gunShotVFX;

    private int randomEnemyIndex;

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
        if (enemySpawner.currentEnemies.Count != 0 && randomEnemyIndex < enemySpawner.currentEnemies.Count)
        {
            EnemyController targetedEnemy = enemySpawner.currentEnemies[randomEnemyIndex];

            Quaternion targetRotation = Quaternion.LookRotation(transform.position - targetedEnemy.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void DealDamage()
    {
        randomEnemyIndex = Random.Range(0, enemySpawner.currentEnemies.Count);

        if (enemySpawner.currentEnemies.Count != 0)
        {
            EnemyController targetedEnemy = enemySpawner.currentEnemies[randomEnemyIndex];

            targetedEnemy.TakeDamage(damagePerHit, targetedEnemy.transform.position + new Vector3(0, 1.5f, 0));
            gunShotVFX.Play();
        }
    }
}
