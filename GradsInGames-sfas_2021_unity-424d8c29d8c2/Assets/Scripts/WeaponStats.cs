using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStats
{
    public float fireRate = 1;
    public int magazineMAx = 0;
    public int currentMagazine = 0;
    public float range = 1;
    public int damage = 1;
    public float reloadSpeed = 0;
    public float critChance = 1;
    public float bulletSpeed = 1000;
    public EGun gunType = EGun.NoGun;
    public float gunHeight = 1.5f;

    public abstract void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects);
    public void Shoot(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, LayerMask hitObjects)
    {
        var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = ((destination - particleSystem.transform.position)).normalized * (bulletSpeed * Time.deltaTime);

        particleSystem.Play();
        RaycastHit hit;
        if (Physics.Raycast(particleSystem.transform.position - particleSystem.transform.forward, destination - particleSystem.transform.position, out hit, range, hitObjects))
        {
            if (hit.distance < range)
            {
                //tracer.transform.Translate(Vector3.forward);
                if (CritChance())
                {
                    hit.collider.gameObject.GetComponentInParent<Health>().TakeDamage(100);
                }
                else
                {
                    hit.collider.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
                }
            }
        }
    }

    public bool CritChance()
    {
        int chance = Random.Range(0, 100);
        if (chance <= critChance)
        {
            Debug.Log("WOW CRIT");
            return true;
        }
        return false;
    }

}

public class Shotgun : WeaponStats
{
    public int pellets = 6;
    public float pelletSpread = 3.5f;

    public Shotgun()
    {
        fireRate = 0.7f;
        reloadSpeed = 1;
        damage = 25;
        critChance = 5;
        magazineMAx = 6;
        currentMagazine = magazineMAx;
        range = 3;
        gunType = EGun.Shotgun;
    }

    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
        if (currentMagazine > 0)
        {
            for (int i = 0; i < pellets; i++)
            {
                Vector3 offset = Random.insideUnitSphere;
                offset.y = destination.y;
                Vector3 dir = Vector3.zero;

                if (Vector3.Distance(destination, gunPos) > 1f)
                {
                    Vector3 direction = destination - particleSystem.transform.position;
                    direction = particleSystem.transform.position + (direction.normalized * 2);
                    offset = offset + direction;
                    destination.y = particleSystem.transform.position.y;


                    Vector3 bulletDir = offset - particleSystem.transform.position;
                    dir = bulletDir;
                    var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = (bulletDir).normalized * (bulletSpeed * Time.deltaTime);
                }
                else
                {
                    offset.y = particleSystem.transform.forward.y;
                    dir = (particleSystem.transform.forward + (offset + (particleSystem.transform.forward.normalized * 3)));
                    var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = dir.normalized * (bulletSpeed * Time.deltaTime);
                }

                particleSystem.Play();

                ShotgunDamage(dir.normalized, hitObjects, particleSystem.transform.position - particleSystem.transform.forward);

            }
            currentMagazine--;
        }
    }

    public void ShotgunDamage(Vector3 dir, LayerMask hitObjects, Vector3 origin)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, range, hitObjects))
        {
            if (hit.distance < range)
            {
                if (CritChance())
                {
                    hit.collider.gameObject.GetComponentInParent<Health>().TakeDamage(100);
                }
                else
                {
                    if ((int)hit.distance == 0)
                    {
                        hit.collider.gameObject.GetComponentInParent<Health>().TakeDamage(damage);
                    }
                    else
                    {
                        Debug.Log(hit.distance);
                        //tracer.transform.Translate(Vector3.forward);
                        hit.collider.gameObject.GetComponentInParent<Health>().TakeDamage(damage / (int)hit.distance);
                    }
                }
            }
        }
    }

}

public class Rifle : WeaponStats
{
    public Rifle()
    {
        fireRate = 0.3f;
        reloadSpeed = 1;
        damage = 15;
        critChance = 5;
        magazineMAx = 30;
        currentMagazine = magazineMAx;
        range = 20;
        gunType = EGun.Rifle;
    }

    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
        if (currentMagazine > 0)
        {
            Shoot(particleSystem, tracer, destination, hitObjects);
            currentMagazine--;
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
        critChance = 10;
        magazineMAx = 15;
        currentMagazine = magazineMAx;
        range = 15;
        gunType = EGun.Pistol;
    }

    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
        if (currentMagazine > 0)
        {
            Shoot(particleSystem, tracer, destination, hitObjects);
            currentMagazine--;            
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

    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
    }
}

