using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    private static GunManager _instance;

    public static GunManager Instance { get { return _instance; } }


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

    public WeaponStats currentlyEquipped;
    public List<WeaponStats> gunList;

    public EGun currentGun = EGun.NoGun;
    [HideInInspector]
    public EGun oldGun = EGun.NoGun;

    public EGun _Gun1 = EGun.Rifle;
    public EGun _Gun2 = EGun.Shotgun;

    [HideInInspector]
    public ParticleSystem particleSystem;

    public int _MAXAmmoReserves = 300;
    public int _AmmoReserves = 300;

    // Start is called before the first frame update
    void Start()
    {
        gunList = new List<WeaponStats>();
        gunList.Add(new NoGun());
        gunList.Add(new Shotgun());
        gunList.Add(new Pistol());
        gunList.Add(new Rifle());
        gunList.Add(new NoGun());
        gunList.Add(new NoGun());
        gunList.Add(new Shotgun());
        gunList.Add(new Pistol());
        gunList.Add(new Rifle());
        currentlyEquipped = gunList[(int)currentGun];
    }

    // Update is called once per frame
    void Update()
    {
        if (oldGun != currentGun)
        {
            currentlyEquipped = gunList[(int)currentGun];
            EGun temp = currentGun;

            Debug.Log(temp);

            if(currentGun > EGun.AmountOfGuns)
            {
                temp -= 5;
            }

            if (currentGun != EGun.NoGun && currentGun != EGun.Torch)
            {
                particleSystem = GameObject.FindGameObjectWithTag(temp.ToString()).GetComponentInChildren<ParticleSystem>();
            }
            oldGun = currentGun;
        }



    }
}
