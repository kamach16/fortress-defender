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
    [SerializeField] private int currentAmmo;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject groundHitSplatVFX;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AmmoDisplay ammoDisplay;

    private void Start()
    {
        SelectWeapon(currentWeapon); // delete this when shop will be in building
    }

    private void Update()
    {
        Shooting();
        Reloading();
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        damagePerHit = currentWeapon.damagePerHit;
        maxAmmo = currentWeapon.maxAmmo;
        reloadTime = currentWeapon.reloadTime;
        isRifle = currentWeapon.fullAuto;

        currentAmmo = maxAmmo;
        ammoDisplay.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    private void Shooting()
    {
        if (gameManager.GetIfLost()) return;

        if(Input.GetMouseButtonDown(0))
        {
            if (currentAmmo <= 0) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            currentAmmo--;
            ammoDisplay.UpdateAmmoText(currentAmmo, maxAmmo);

            if (Physics.Raycast(ray, out hit))
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

    private void Reloading()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentAmmo = maxAmmo;
            ammoDisplay.UpdateAmmoText(currentAmmo, maxAmmo);
        }
    }

    private void CreateGroundHitSplatVFX(Vector3 spawnPos)
    {
        GameObject vfx = Instantiate(groundHitSplatVFX, spawnPos, Quaternion.identity);
        Destroy(vfx, 1f);
    }
}
