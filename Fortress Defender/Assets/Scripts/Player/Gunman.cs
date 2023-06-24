using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float minTimeBetweenShoots;
    [SerializeField] private float maxTimeBetweenShoots;
    [SerializeField] private float damagePerHit;
    
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
        int randomEnemyIndex = Random.Range(0, enemySpawner.currentEnemies.Count);

        if (enemySpawner.currentEnemies.Count != 0)
        {
            EnemyController targetedEnemy = enemySpawner.currentEnemies[randomEnemyIndex];

            targetedEnemy.TakeDamage(damagePerHit, targetedEnemy.transform.position + new Vector3(0, 1.5f, 0));
        }
    }
}
