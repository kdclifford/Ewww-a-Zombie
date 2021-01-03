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
            if (gunManager.currentGun != gunManager._Gun1)
            {
                EquptGun(gunManager._Gun1);
            }
            else if (gunManager.currentGun != gunManager._Gun2)
            {
                EquptGun(gunManager._Gun2);
            }
        }
    }

    void EquptGun(EGun gun)
    {
        _GunPose.SetInteger("Gun", (int)gun);
    }

    public void CurrentPistol()
    {
        gunManager.currentGun = EGun.Pistol;
    }

    public void CurrentShotgun()
    {
        gunManager.currentGun = EGun.Shotgun;
    }

    public void CurrentRifle()
    {
        gunManager.currentGun = EGun.Rifle;
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
}