using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        Destroy(gameObject, 3); // if didnt hit anything
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>()) DealDamage(other.GetComponent<EnemyController>());
    }

    private void DealDamage(EnemyController enemy)
    {
        enemy.TakeDamage(damage, enemy.transform.position + new Vector3(0, 1.5f, 0));

        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
