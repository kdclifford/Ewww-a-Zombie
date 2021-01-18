using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStats
{
    public float MaxFireRate = 1;
    public float StartFireRate = 1;
    public float fireRate = 1;
    public int MaxMag = 0;
    public int startMagazine = 0;
    public int fullMag = 0;
    public int currentMagazine = 0;
    public float StartRange = 1;
    public float MaxRange = 1;
    public float range = 1;
    public int StartDamage = 1;
    public int MaxDamage = 1;
    public int damage = 1;
    public float reloadSpeed = 0;
    public float StartCritChance = 1;
    public float MaxCritChance = 1;
    public float critChance = 1;
    public float bulletSpeed = 1000;
    public EGun gunType = EGun.NoGun;
    public float gunHeight = 1.5f;

   public int MaxUpgrades = 10;
    public int firerateUpgrade = 0;
    public int rangeUpgrade = 0;
    public int damageUpgrade = 0;
    public int magUpgrade = 0;
    public int critUpgrade = 0;

    public abstract void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects);
    public abstract void RandomStats();
    public void Shoot(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, LayerMask hitObjects)
    {

        var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = ((destination - particleSystem.transform.position)).normalized * (bulletSpeed * Time.deltaTime);

        particleSystem.Play();
        RaycastHit hit;
        if (Physics.Raycast(particleSystem.transform.position - particleSystem.transform.forward, destination - particleSystem.transform.position, out hit, range, hitObjects))
        {
            if (hit.collider.gameObject.tag != "Invisible")
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
                    HitColour(hit.collider.gameObject);
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

    public void HitColour(GameObject agent)
    {
        agent.transform.parent.GetComponent<Animator>().SetTrigger("Colour");
    }

}

public class Shotgun : WeaponStats
{
    public int pellets = 6;
    public float pelletSpread = 3.5f;

    public Shotgun()
    {
        MaxFireRate = 0.1f;
        MaxDamage = 50;
        MaxCritChance = 20;
        MaxMag = 10;
        MaxRange = 15;

        startMagazine = 6;
        StartFireRate = 0.7f;
        StartDamage = 35;
        StartCritChance = 5;
        StartRange = 7;
        fullMag = startMagazine;

        fireRate = StartFireRate;
        damage = StartDamage;
        critChance = StartCritChance;
        currentMagazine = startMagazine;
        range = StartRange;

        reloadSpeed = 1;
        gunType = EGun.Shotgun;
    }

    public override void RandomStats()
    {
        fireRate = Random.Range(0.4f, 1.5f);
        reloadSpeed = Random.Range(0.5f, 2);
        damage = Random.Range(25, 50);
        critChance = Random.Range(4, 7);
        startMagazine = Random.Range(1, 8);
        currentMagazine = startMagazine;
        range = Random.Range(5, 10);
        pellets = Random.Range(4, 8);
        pelletSpread = Random.Range(1.5f, 4f);
    }


    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
        if (currentMagazine > 0)
        {
            SoundManager.instance.PlayOnceAtPoint(ESoundClipEnum.Gun3Cut, particleSystem.gameObject);
            for (int i = 0; i < pellets; i++)
            {
                Vector3 offset = Random.insideUnitSphere;
                offset.y = 0;
                // offset *= pelletSpread;

                Vector3 dir = Vector3.zero;

                if (Vector3.Distance(destination, gunPos) > 1f)
                {
                    Vector3 direction = (destination - particleSystem.transform.position).normalized;
                    direction = particleSystem.transform.position + (direction * range);
                    direction += offset * pelletSpread;
                    direction = direction - particleSystem.transform.position;
                    dir = direction;

                    var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = (direction).normalized * (bulletSpeed * Time.deltaTime);
                    //bullet.GetComponent<Rigidbody>().velocity = ((destination - particleSystem.transform.position)).normalized * (bulletSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 direction = ((particleSystem.transform.position + (particleSystem.transform.forward * range)) - particleSystem.transform.position).normalized;
                    direction = particleSystem.transform.position + (direction * range);
                    direction += offset * pelletSpread;
                    direction = direction - particleSystem.transform.position;
                    dir = direction;

                    var bullet = MonoBehaviour.Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = (dir).normalized * (bulletSpeed * Time.deltaTime);
                    // bullet.GetComponent<Rigidbody>().velocity = ((destination - particleSystem.transform.position)).normalized * (bulletSpeed * Time.deltaTime);
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
            if (hit.collider.gameObject.tag != "Invisible")
            {
                if (hit.distance < range)
                {
                    Health health = hit.collider.gameObject.GetComponentInParent<Health>();
                    if (CritChance())
                    {
                        health.TakeDamage(100);
                    }
                    else
                    {
                        if ((int)hit.distance == 0)
                        {
                            health.TakeDamage(damage);
                        }
                        else
                        {
                            Debug.Log(hit.distance);
                            //tracer.transform.Translate(Vector3.forward);
                            health.TakeDamage(damage / (int)hit.distance);
                        }
                    }
                    HitColour(hit.collider.gameObject);
                }
            }
        }
    }

}

public class Rifle : WeaponStats
{
    public Rifle()
    {
        MaxFireRate = 0.1f;
        MaxDamage = 30;
        MaxCritChance = 20;
        MaxMag = 50;
        MaxRange = 30;

        startMagazine = 30;
        StartFireRate = 0.3f;
        StartDamage = 15;
        StartCritChance = 5;
        StartRange = 20;
        fullMag = startMagazine;

        fireRate = StartFireRate;
        damage = StartDamage;
        critChance = StartCritChance;
        currentMagazine = startMagazine;
        range = StartRange;

        reloadSpeed = 1;
        gunType = EGun.Rifle;
    }

    public override void RandomStats()
    {
        fireRate = Random.Range(0.2f, 1f);
        reloadSpeed = Random.Range(0.5f, 2);
        damage = Random.Range(12, 30);
        critChance = Random.Range(5, 10);
        startMagazine = Random.Range(20, 45);
        currentMagazine = startMagazine;
        range = Random.Range(15, 30);
    }


    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
        if (currentMagazine > 0)
        {
            SoundManager.instance.PlayOnceAtPoint(ESoundClipEnum.Gun1Cut, particleSystem.gameObject);
            Shoot(particleSystem, tracer, destination, hitObjects);
            currentMagazine--;
        }
    }
}

public class Pistol : WeaponStats
{
    public Pistol()
    {
        MaxFireRate = 0.01f;
        MaxDamage = 50;
        MaxCritChance = 25;
        MaxMag = 22;
        MaxRange = 24;

        startMagazine = 15;
        StartFireRate = 0.5f;
        StartDamage = 34;
        StartCritChance = 10;
        StartRange = 15;
        fullMag = startMagazine;

        fireRate = StartFireRate;
        damage = StartDamage;
        critChance = StartCritChance;
        currentMagazine = startMagazine;
        range = StartRange;

        reloadSpeed = 1;        
        gunType = EGun.Pistol;
    }

    public override void RandomStats()
    {
        fireRate = Random.Range(0.4f, 1f);
        reloadSpeed = Random.Range(0.5f, 2);
        damage = Random.Range(25, 34);
        critChance = Random.Range(7, 15);
        startMagazine = Random.Range(12, 20);
        currentMagazine = startMagazine;
        range = Random.Range(12, 20);
    }




    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
        if (currentMagazine > 0)
        {
            SoundManager.instance.PlayOnceAtPoint(ESoundClipEnum.Gun4Cut, particleSystem.gameObject);
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
        startMagazine = 0;
        range = 0;
        gunType = EGun.NoGun;
    }

    public override void RandomStats()
    {
    }


    public override void Fire(ParticleSystem particleSystem, GameObject tracer, Vector3 destination, Vector3 gunPos, LayerMask hitObjects)
    {
    }
}

