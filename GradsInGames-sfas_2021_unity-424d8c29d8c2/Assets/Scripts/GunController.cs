using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private static GunController _instance;

    public static GunController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }



    private GunManager gunManager;
    public GameObject rifle;
    public GameObject pistol;
    public GameObject shotgun;

    PlayerController playerController;
    PlayerAnimations playerAnimations;

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
        playerAnimations = transform.GetComponentInChildren<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        //Reload gun
        if (!reloadGun && GunManager.Instance.currentlyEquipped.currentMagazine < GunManager.Instance.currentlyEquipped.fullMag && GunManager.Instance._AmmoReserves > 0)
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
                tempMagAmmo = GunManager.Instance.currentlyEquipped.fullMag - GunManager.Instance.currentlyEquipped.currentMagazine;

                if (GunManager.Instance._AmmoReserves + GunManager.Instance.currentlyEquipped.currentMagazine >= GunManager.Instance.currentlyEquipped.fullMag)
                {
                    GunManager.Instance.currentlyEquipped.currentMagazine = GunManager.Instance.currentlyEquipped.fullMag;
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
            if (gunManager.currentGun != EGun.NoGun && gunManager.currentGun != EGun.Torch && Input.GetMouseButton(0) && timer < 0)
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

    public void SelectGun(EGun gun)
    {
        EGun tempGun = gun;
        if(tempGun > EGun.AmountOfGuns)
        {
            tempGun -= 5;
        }


        if(!CheckForDupeGun(gun))
        {
           
        }
        else if (GunManager.Instance._Gun1 == EGun.NoGun)
        {
            GunManager.Instance._Gun1 = gun;
            DisableGun(GunManager.Instance._Gun1, tempGun);
        }
        else if (GunManager.Instance._Gun2 == EGun.NoGun)
        {
            GunManager.Instance._Gun2 = gun;           
                DisableGun(GunManager.Instance._Gun2, tempGun);            
        }
        else if (GunManager.Instance.currentGun != EGun.Torch)
        {
            if (gun != GunManager.Instance._Gun1 && gun != GunManager.Instance._Gun2 || tempGun != GunManager.Instance._Gun1 && tempGun != GunManager.Instance._Gun2)
            {
                if (GunManager.Instance.currentGun == GunManager.Instance._Gun1)
                {
                    GunManager.Instance._Gun1 = gun;
                }
                else if (GunManager.Instance.currentGun == GunManager.Instance._Gun2)
                {
                    GunManager.Instance._Gun2 = gun;

                }                
                    DisableGun(GunManager.Instance.currentGun, tempGun);                
            }
        }
        else
        {

          
                DisableGun(GunManager.Instance._Gun1, tempGun);
           
            GunManager.Instance._Gun1 = gun;
        }
        GunManager.Instance.currentGun = gun;
        playerAnimations.EquptGun(gun);
    }

    bool CheckForDupeGun(EGun gun)
    {
        if(GunManager.Instance._Gun1 == gun)
        {
            return false;   
        }

        if (GunManager.Instance._Gun2 == gun)
        {
            return false;
        }

        if(gun > EGun.AmountOfGuns)
        {
            if (GunManager.Instance._Gun1 == gun - 5)
            {
                GunManager.Instance._Gun1 = gun;
                return false;
            }

            if (GunManager.Instance._Gun2 == gun - 5)
            {
                GunManager.Instance._Gun2 = gun;
                return false;
            }
        }
        else
        {
            if (GunManager.Instance._Gun1 == gun + 5)
            {
                GunManager.Instance._Gun1 = gun;
                return false;
            }

            if (GunManager.Instance._Gun2 == gun + 5)
            {
                GunManager.Instance._Gun2 = gun;
                return false;
            }
        }

        return true;
    }



    void DisableGun(EGun oldGun, EGun newGun)
    {       
        if (oldGun == EGun.Shotgun)
        {
            shotgun.SetActive(false);
        }
        else if (oldGun == EGun.Pistol)
        {
            pistol.SetActive(false);
        }
        else if (oldGun == EGun.Rifle)
        {
            rifle.SetActive(false);
        }

        if (newGun == EGun.Shotgun)
        {
            shotgun.SetActive(true);
        }
        else if (newGun == EGun.Pistol)
        {
            pistol.SetActive(true);
        }
        else if (newGun == EGun.Rifle)
        {
            rifle.SetActive(true);
        }
    }


}
