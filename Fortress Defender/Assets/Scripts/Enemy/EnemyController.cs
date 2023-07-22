using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("About movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private bool isStopped = false;

    [Header("About health")]
    [SerializeField] private float health;
    [SerializeField] private bool isDied = false;
    [SerializeField] private int moneyToAdd;
    [SerializeField] private ParticleSystem hitSplatVFX;

    [Header("About shooting")]
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem gunShotVFX;

    [Header("Others")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider[] myColliders;
    [SerializeField] private Transform fortress;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource audioSource;

    private EnemySpawner enemySpawner;

    private void Update()
    {
        Move();
        StopEnemy();
    }

    public void SetFortress(Transform fortress)
    {
        this.fortress = fortress;
    }

    public void SetPlayerHealth(PlayerHealth playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    public void SetEnemySpawner(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    // MOVEMENT SECTION
    private void Move()
    {
        if (isStopped || isDied) return;

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void StopEnemy()
    {
        if (isStopped) return;

        if (transform.position.x > fortress.position.x - stopDistance)
        {
            Shooting();
            isStopped = true;
        }
    }

    // SHOOTING SECTION
    private void Shooting()
    {
        animator.SetTrigger("shoot");
    }

    public void DoDamage() // animation event
    {
        gunShotVFX.Play();
        audioSource.Play();

        if (playerHealth != null) playerHealth.TakeDamage(damage);
    }

    // HEALTH SECTION
    private void Die()
    {
        animator.SetTrigger("death");
        DisableColliders();
        isDied = true;
        enemySpawner.DeleteEnemy(this);
        gameManager.AddMoney(moneyToAdd);
        Destroy(gameObject, 2f);
    }

    private void DisableColliders()
    {
        foreach (Collider collider in myColliders)
        {
            collider.enabled = false;
        }
    }

    public void TakeDamage(float damage, Vector3 newhitSplatVFXPos)
    {
        if (isDied) return;

        health -= damage;
        ShowEnemyHitSplat(newhitSplatVFXPos);

        if (health <= 0) Die();
    }

    public void ShowEnemyHitSplat(Vector3 newhitSplatVFXPos)
    {
        hitSplatVFX.transform.position = newhitSplatVFXPos;
        hitSplatVFX.Play();
    }
}
