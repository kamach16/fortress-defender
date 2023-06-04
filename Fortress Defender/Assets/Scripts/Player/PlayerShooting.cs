using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private float damagePerHit;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool isRifle;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject groundHitSplatVFX;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        Shooting();
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        damagePerHit = currentWeapon.damagePerHit;
        maxAmmo = currentWeapon.maxAmmo;
        reloadTime = currentWeapon.reloadTime;
        isRifle = currentWeapon.isRifle;
    }

    private void Shooting()
    {
        if (gameManager.GetIfLost()) return;

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
                    enemy.TakeDamage(damagePerHit);
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
