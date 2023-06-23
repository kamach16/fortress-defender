using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private List<EnemyController> hits;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask enemyLayer;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        RaycastHit[] sphereHits = Physics.SphereCastAll(transform.position, explosionRadius, transform.position, 0, enemyLayer);

        hits.Clear();
        foreach (RaycastHit hit in sphereHits)
        {
            if (hit.transform.GetComponent<EnemyController>()) hits.Add(hit.transform.GetComponent<EnemyController>());            
        }
    }
}
