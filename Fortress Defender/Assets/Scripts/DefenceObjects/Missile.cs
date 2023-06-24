using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private List<EnemyController> nearbyEnemies;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private GameObject model;

    private void Update()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        RaycastHit[] sphereHits = Physics.SphereCastAll(transform.position, explosionRadius, transform.position, 0, enemyLayer); // get all objects within explosionRadius

        nearbyEnemies.Clear();
        foreach (RaycastHit hit in sphereHits)
        {
            if (hit.transform.GetComponent<EnemyController>()) nearbyEnemies.Add(hit.transform.GetComponent<EnemyController>()); // get only enemies          
        }

        DealDamage();
    }

    private void DealDamage()
    {
        foreach (EnemyController enemy in nearbyEnemies)
        {
            enemy.TakeDamage(damage, enemy.transform.position + new Vector3(0, 1.5f, 0));
        }

        explosionVFX.Play();
        model.SetActive(false);
        Destroy(gameObject, 3);
    }

    private void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
