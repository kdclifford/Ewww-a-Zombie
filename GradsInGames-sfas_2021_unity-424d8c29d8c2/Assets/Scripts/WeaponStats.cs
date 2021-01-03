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
    public float bulletSpeed = 3000;
    public EGun gunType = EGun.NoGun;
    public abstract void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, LayerMask hitObjects);
}

public class Shotgun : WeaponStats
{
    public int pellets = 6;
    public float pelletSpread = 1f;
    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, LayerMask hitObjects)
    {

        for (int i = 0; i < pellets; i++)
        {
            var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = (((destination +
                Random.insideUnitSphere * pelletSpread) - particleSystem.transform.position)).normalized * (bulletSpeed * Time.deltaTime);

            particleSystem.Play();
            RaycastHit hit;
            if (Physics.Raycast(particleSystem.transform.position, destination - particleSystem.transform.position, out hit, range, hitObjects))
            {
                Debug.Log(hit.transform.name);
                //tracer.transform.Translate(Vector3.forward);
            }
        }

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
        range = 1;
        gunType = EGun.Rifle;
    }

    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, LayerMask hitObjects)
    {



        var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = ((destination - particleSystem.transform.position)).normalized * (bulletSpeed * Time.deltaTime);

        particleSystem.Play();
            RaycastHit hit;
            if (Physics.Raycast(particleSystem.transform.position, destination - particleSystem.transform.position, out hit, range, hitObjects))
            {
                Debug.Log(hit.transform.name);
                //tracer.transform.Translate(Vector3.forward);
            }
        

    }
}

public class Pistol : WeaponStats
{
    public Pistol()
    {
        fireRate = 0.5f;
        reloadSpeed = 1;
        damage = 10;
        critChance = 20;
        magazineMAx = 15;
        range = 1;
        gunType = EGun.Pistol;
    }

    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, LayerMask hitObjects)
    {
        var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = ((destination - particleSystem.transform.position)).normalized * (bulletSpeed * Time.deltaTime);

        particleSystem.Play();
        RaycastHit hit;
        if (Physics.Raycast(particleSystem.transform.position, destination - particleSystem.transform.position, out hit, range, hitObjects))
        {
            Debug.Log(hit.transform.name);
            //tracer.transform.Translate(Vector3.forward);
        }


    }
}


public class NoGun : WeaponStats
{
    public NoGun()
    {
        fireRate = 0f;
        reloadSpeed = 0;
        damage = 0;
        critChance = 0;
        magazineMAx = 0;
        range = 0;
        gunType = EGun.NoGun;
    }

    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, LayerMask hitObjects)
    {
    }
}

