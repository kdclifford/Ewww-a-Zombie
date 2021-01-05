using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
   private GunManager gunManager;

    PlayerController playerController;

    Vector3 endPoint;
   public LayerMask layerMask;

    float timer = 0;

    public GameObject tracer;

    bool reloadGun = false;
    float reloadTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        gunManager = GunManager.Instance;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Reload gun
        if (!reloadGun && GunManager.Instance.currentlyEquipped.currentMagazine < GunManager.Instance.currentlyEquipped.magazineMAx && GunManager.Instance._AmmoReserves > 0)
        {
            if (Input.GetKey(KeyCode.R) || GunManager.Instance.currentlyEquipped.currentMagazine == 0)
            {
                reloadTimer = GunManager.Instance.currentlyEquipped.reloadSpeed;
                reloadGun = true;
            }
        }

        if (reloadGun)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                    int tempMagAmmo;
                    tempMagAmmo = GunManager.Instance.currentlyEquipped.magazineMAx - GunManager.Instance.currentlyEquipped.currentMagazine;

                if (GunManager.Instance._AmmoReserves + GunManager.Instance.currentlyEquipped.currentMagazine >= GunManager.Instance.currentlyEquipped.magazineMAx)
                {
                    GunManager.Instance.currentlyEquipped.currentMagazine = GunManager.Instance.currentlyEquipped.magazineMAx;
                    GunManager.Instance._AmmoReserves -= tempMagAmmo;
                }
                else
                {
                    GunManager.Instance.currentlyEquipped.currentMagazine += GunManager.Instance._AmmoReserves;
                    GunManager.Instance._AmmoReserves = 0;
                }
                reloadGun = false;
            }
        }



        if (!reloadGun)
        {
            if (gunManager.currentGun != EGun.NoGun && gunManager.currentGun != EGun.FlashLight && Input.GetKey(KeyCode.Space) && timer < 0)
            {
                Vector3 newGunPos = transform.position;
                newGunPos.y = transform.position.y + gunManager.currentlyEquipped.gunHeight;
                if (Vector3.Distance(newGunPos, playerController.point) > 0.7f || gunManager.currentGun == EGun.Shotgun)
                {
                    gunManager.currentlyEquipped.Fire(gunManager.particleSystem, tracer, playerController.point, newGunPos, layerMask);
                    timer = gunManager.currentlyEquipped.fireRate;
                }
                else
                {
                    gunManager.currentlyEquipped.Fire(gunManager.particleSystem, tracer, gunManager.particleSystem.transform.forward, newGunPos, layerMask);
                    timer = gunManager.currentlyEquipped.fireRate;
                }
            }
            Debug.DrawRay(transform.position, (playerController.point - transform.position) * 10, Color.green);

            timer -= Time.deltaTime;
        }
    }
}
