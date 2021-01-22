using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunUI : MonoBehaviour
{
    private TMP_Text _GunName;
    private TMP_Text _Range;
    private TMP_Text _Firerate;
    private TMP_Text _Damage;
    private TMP_Text _MagSize;
    private TMP_Text _Crit;

    // Start is called before the first frame update
    void Start()
    {
        _GunName = transform.GetChild(0).GetComponent<TMP_Text>();
        _Range = transform.GetChild(1).GetComponent<TMP_Text>();
        _Firerate = transform.GetChild(2).GetComponent<TMP_Text>();
        _Damage = transform.GetChild(3).GetComponent<TMP_Text>();
        _MagSize = transform.GetChild(4).GetComponent<TMP_Text>();
        _Crit = transform.GetChild(5).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(GunManager.Instance.currentGun != EGun.NoGun && GunManager.Instance.currentGun != EGun.FlashLight)
        {
            CurrentGun(GunManager.Instance.currentlyEquipped);
        }
        else if(GunManager.Instance.currentGun > EGun.AmountOfGuns)
        {
            _GunName.text = "Cannot Upgrade Random Guns";
        }

    }

    void CurrentGun (WeaponStats gun)
    {
        _GunName.text = gun.gunType.ToString();
        _Damage.text = "Damage = " + gun.damage.ToString();
        _Firerate.text = "Firerate = " + gun.fireRate.ToString();
        _Crit.text = "Crit Chance = " + gun.critChance.ToString();
        _Range.text = "Range = " + gun.range.ToString();
        _MagSize.text = "Mag Size = " + gun.fullMag.ToString();
    }


}
