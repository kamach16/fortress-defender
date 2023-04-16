using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                EnemyController enemy = hit.transform.gameObject.GetComponent<EnemyController>();

                if (enemy == null) return;
                enemy.TakeDamage(damage);
            }
        }
    }
}
