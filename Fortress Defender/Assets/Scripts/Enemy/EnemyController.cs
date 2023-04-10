using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask stopTriggerLayer;
    [SerializeField] private bool canMove = true;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove) return;

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void Shooting()
    {
        animator.SetTrigger("shoot");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.GetMask(LayerMask.LayerToName(other.gameObject.layer)) == stopTriggerLayer)
        {
            Shooting();
            canMove = false;
        }
    }
}
