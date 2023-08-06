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
    [SerializeField] private AudioSource audioSource;

    private EnemySpawner enemySpawner;

    private int randomEnemyIndex;

    private void Awake()
    {
        DestroyAudioSourceIfNotNeeded();
        SetVariables();
    }

    private void OnEnable()
    {
        GameManager.OnDefeat += DeactiveDefenceObject;
    }

    private void OnDisable()
    {
        GameManager.OnDefeat -= DeactiveDefenceObject;
    }

    private void DeactiveDefenceObject()
    {
        isActive = false;
        if (audioSource != null) audioSource.Stop();
    }

    private void SetVariables()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void DestroyAudioSourceIfNotNeeded()
    {
        if (FindObjectsOfType<HeavyGunDefenceObject>().Length > 1) Destroy(audioSource); // this condition checks if there is more than one heavy gun defence object
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

            if (targetedEnemy == null) return;

            Quaternion targetRotation = Quaternion.LookRotation(transform.position - targetedEnemy.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (audioSource != null && !audioSource.isPlaying) audioSource.Play();
        }
        else
        {
            if (audioSource != null) audioSource.Stop();
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
