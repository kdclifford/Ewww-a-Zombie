using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _GunPose;
    public EGun currentGun = EGun.NoGun;
    private EGun _Gun1 = EGun.Rifle;
    private EGun _Gun2 = EGun.Pistol;

    //public GameObject _PistolRightHand;
    //public GameObject _PistolLeftHand;
    //public GameObject _ShotgunRightHand;
    //public GameObject _ShotgunLeftHand;
    //public TwoBoneIKConstraint _RightHand;
    //public TwoBoneIKConstraint _LefttHand;

    // Start is called before the first frame update
  
        void Start()
        {
            _GunPose = GetComponent<Animator>();
            _GunPose.enabled = false;
            _GunPose.enabled = true;
        }
    

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.T))
        {
            EquptGun(EGun.FlashLight);
        }
      else if(Input.GetKey(KeyCode.I))
        {
            if(currentGun != _Gun1)
            {
                EquptGun(_Gun1);
            }
            else if(currentGun != _Gun2)
            {                
                EquptGun(_Gun2);
            }
        }
    }

    void EquptGun(EGun gun)
    {
        _GunPose.SetInteger("Gun", (int)gun);
    }

    public void CurrentPistol()
    {
        currentGun = EGun.Pistol;
    }

    public void CurrentShotgun()
    {
        currentGun = EGun.Shotgun;
    }

    public void CurrentRifle()
    {
        currentGun = EGun.Rifle;
    }

    public void CurrentNoGun()
    {
        currentGun = EGun.NoGun;
    }
}

public enum EGun
{
    NoGun = 0,
    Shotgun,
    Pistol,
    Rifle,
    FlashLight,
    AmountOfGuns,
}