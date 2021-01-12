using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _GunPose;
    private GunManager gunManager;


    // Start is called before the first frame update

    void Start()
    {
        gunManager = GunManager.Instance;
        _GunPose = GetComponent<Animator>();
        _GunPose.enabled = false;
        _GunPose.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            EquptGun(EGun.FlashLight);
        }
        else if (Input.GetKey(KeyCode.I))
        {
            EGun gun1 = gunManager._Gun1;
            EGun gun2 = gunManager._Gun2;

            //if(gun1 > EGun.AmountOfGuns)
            //{
            //    gun1 -= 5;
            //}

            //if (gun2 > EGun.AmountOfGuns)
            //{
            //    gun2 -= 5;
            //}

            if (gunManager.currentGun != gun1)
            {                
                EquptGun(gunManager._Gun1);
            }
            else if (gunManager.currentGun != gun2)
            {
                EquptGun(gunManager._Gun2);
            }
        }
    }

   public void EquptGun(EGun gun)
    {
        if (gun < EGun.AmountOfGuns)
        {
            _GunPose.SetInteger("Gun", (int)gun);
        }
        else
        {
            _GunPose.SetInteger("Gun", (int)(gun - 5));
        }
    }

    public void CurrentPistol()
    {
        if (gunManager._Gun1 - 5 == EGun.Pistol || gunManager._Gun2 - 5 == EGun.Pistol)
        {
            gunManager.currentGun = EGun.CustomPistol;
        }
        else
        {
            gunManager.currentGun = EGun.Pistol;
        }
    }

    public void CurrentShotgun()
    {
        if (gunManager._Gun1 - 5 == EGun.Shotgun || gunManager._Gun2 - 5 == EGun.Shotgun)
        {
            gunManager.currentGun = EGun.CustomShotgun;
        }
        else
        {
            gunManager.currentGun = EGun.Shotgun;
        }
    }

    public void CurrentRifle()
    {
        if (gunManager._Gun1 - 5 == EGun.Rifle || gunManager._Gun2 - 5 == EGun.Rifle)
        {
            gunManager.currentGun = EGun.CustomRifle;
        }
        else
        {
            gunManager.currentGun = EGun.Rifle;
        }
    }

    public void CurrentFlashLight()
    {
        gunManager.currentGun = EGun.FlashLight;
    }

    public void CurrentNoGun()
    {
        gunManager.currentGun = EGun.NoGun;
    }
}

public enum EGun
{
    NoGun = 0,
    Shotgun = 1,
    Pistol = 2,
    Rifle = 3,
    FlashLight = 4,
    AmountOfGuns,
    CustomShotgun,
    CustomPistol,
    CustomRifle,
}