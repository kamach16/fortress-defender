using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private bool isStopped = false; 
    [SerializeField] private Animator animator;
    [SerializeField] private Transform fortress;

    private void Update()
    {
        Move();
        StopEnemy();
    }

    private void Move()
    {
        if (isStopped) return;

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
}
