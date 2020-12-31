using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStats
{
    public float fireRate = 1;
    public float magazineMAx = 0;
    public float range = 1;
    public float damage = 1;
    public float reloadSpeed;
    public float critChance = 1;
    public EGun gunType = EGun.NoGun;
    public abstract void Fire();
}

public class Shotgun : WeaponStats
{
    public int pellets = 6;
    public override void Fire()
    {
      

    }
}

public class Rifle : WeaponStats
{
  public  Rifle()
    {
        fireRate = 0.3f;
        reloadSpeed = 1;
        damage = 25;
        critChance = 20;
        magazineMAx = 30;
        range = 10;
        gunType = EGun.Rifle;
    }

    public override void Fire()
    {


    }
}

