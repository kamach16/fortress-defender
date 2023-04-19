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
    [SerializeField] private ParticleSystem playerWeaponHitSplatVFX;

    [Header("About shooting")]
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem gunShotVFX;

    [Header("Others")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider[] myColliders;
    [SerializeField] private Transform fortress;
    [SerializeField] private PlayerHealth playerHealth;

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
        playerHealth.TakeDamage(damage);
    }

    // HEALTH SECTION
    private void Die()
    {
        animator.SetTrigger("death");
        DisableColliders();
        isDied = true;
        Destroy(gameObject, 2f);
    }

    private void DisableColliders()
    {
        foreach (Collider collider in myColliders)
        {
            collider.enabled = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDied) return;

        health -= damage;

        if (health <= 0) Die();
    }

    public void ShowPlayerWeaponHitSplat(Vector3 newPlayerWeaponHitSplatVFXPos)
    {
        playerWeaponHitSplatVFX.transform.position = newPlayerWeaponHitSplatVFXPos;
        playerWeaponHitSplatVFX.Play();
    }
}
