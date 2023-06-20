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
    [SerializeField] private bool isReloading;

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
        ammoDisplay.UpdateAmmoDisplay(currentAmmo, maxAmmo);
    }

    private void Shooting()
    {
        if (gameManager.GetIfLost() || isReloading) return;

        if(Input.GetMouseButtonDown(0))
        {
            if (currentAmmo <= 0) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            currentAmmo--;
            ammoDisplay.UpdateAmmoDisplay(currentAmmo, maxAmmo);

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
        if (Input.GetMouseButtonDown(1) && !isReloading)
        {
            ammoDisplay.ammoSlider.value = 0;
            ammoDisplay.ammoSlider.maxValue = 1;

            isReloading = true;
        }

        if (isReloading) ReloadAnimation();
    }

    public void ReloadAnimation()
    {
        ammoDisplay.ammoSlider.value += Time.deltaTime / reloadTime;

        if(ammoDisplay.ammoSlider.value >= ammoDisplay.ammoSlider.maxValue)
        {
            isReloading = false;
            ammoDisplay.ammoSlider.maxValue = maxAmmo;
            ammoDisplay.ammoSlider.value = maxAmmo;
            currentAmmo = maxAmmo;
            ammoDisplay.UpdateAmmoDisplay(currentAmmo, maxAmmo);
        }
    }

    private void CreateGroundHitSplatVFX(Vector3 spawnPos)
    {
        GameObject vfx = Instantiate(groundHitSplatVFX, spawnPos, Quaternion.identity);
        Destroy(vfx, 1f);
    }
}
