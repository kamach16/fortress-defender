using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float damagePerHit;
    
    private IEnumerator Start()
    {
        while(true)
        {
            DealDamage();
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    private void DealDamage()
    {
        int randomEnemyIndex = Random.Range(0, enemySpawner.GetCurrentEnemiesList().Count);

        if (enemySpawner.GetCurrentEnemiesList().Count != 0) enemySpawner.GetCurrentEnemiesList()[randomEnemyIndex].TakeDamage(damagePerHit);
    }
}
