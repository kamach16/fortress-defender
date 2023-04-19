using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject groundHitSplatVFX;

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

                if (enemy == null)
                {
                    CreateGroundHitSplatVFX(hit.point);
                }
                else
                {
                    enemy.TakeDamage(damage);
                    enemy.ShowPlayerWeaponHitSplat(hit.point);
                }
            }
        }
    }

    private void CreateGroundHitSplatVFX(Vector3 spawnPos)
    {
        GameObject vfx = Instantiate(groundHitSplatVFX, spawnPos, Quaternion.identity);
        Destroy(vfx, 1f);
    }
}
