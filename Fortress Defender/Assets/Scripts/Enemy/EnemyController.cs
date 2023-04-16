using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float health;
    [SerializeField] private bool isStopped = false;
    [SerializeField] private bool isDied = false;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider[] myColliders;
    [SerializeField] private Transform fortress;

    private void Update()
    {
        Move();
        StopEnemy();
    }

    public void SetFortress(Transform fortress)
    {
        this.fortress = fortress;
    }

    private void Move()
    {
        if (isStopped || isDied) return;

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void StopEnemy()
    {
        if (isStopped) return;

        if(transform.position.x > fortress.position.x - stopDistance)
        {
            Shooting();
            isStopped = true;
        }
    }

    private void Shooting()
    {
        animator.SetTrigger("shoot");
    }

    private void Die()
    {

        animator.SetTrigger("death");
        DisableColliders();
        isDied = true;
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

        print(gameObject.name + " took damage");

        health -= damage;

        if (health <= 0) Die();
    }
}
