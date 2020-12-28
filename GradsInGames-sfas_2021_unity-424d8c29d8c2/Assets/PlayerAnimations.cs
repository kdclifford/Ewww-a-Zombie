using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public Animator _GunPose;
    public EGun currentGun = EGun.NoGun;
    private EGun _Gun1 = EGun.Shotgun;
    private EGun _Gun2 = EGun.Pistol;

    public GameObject _PistolRightHand;
    public GameObject _PistolLeftHand;
    public GameObject _ShotgunRightHand;
    public GameObject _ShotgunLeftHand;
    public TwoBoneIKConstraint _RightHand;
    public TwoBoneIKConstraint _LefttHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.I))
        {
            if(currentGun != EGun.Shotgun)
            {
                EquptGun(EGun.Shotgun);
               // SetShotgun();
            }
            else if(currentGun != EGun.Pistol)
            {
                
                EquptGun(EGun.Pistol);
              //  SetPistol();
            }
        }
    }

    void EquptGun(EGun gun)
    {
        _GunPose.SetInteger("Gun", (int)gun);
    }

    public enum EGun
    {
        NoGun = 0,
        Shotgun,
        Pistol,
        Rifle,
        AmountOfGuns,
    }
 
    //public void SetPistol()
    //{
    //    _RightHand.data.target = _PistolRightHand.transform;
    //    _LefttHand.data.target = _PistolLeftHand.transform;
    //}

    //public void SetShotgun()
    //{
    //    _RightHand.data.target = _ShotgunRightHand.transform;
    //    _LefttHand.data.target = _ShotgunLeftHand.transform;
    //}

    public void CurrentPistol()
    {
        currentGun = EGun.Pistol;
    }

    public void CurrentShotgun()
    {
        currentGun = EGun.Shotgun;
    }

}
