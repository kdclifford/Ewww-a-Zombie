using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    private TMP_Text _AmmoCounter;
    // Start is called before the first frame update
    void Start()
    {
        _AmmoCounter = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GunManager.Instance.currentGun == EGun.NoGun || GunManager.Instance.currentGun == EGun.FlashLight)
        {
            _AmmoCounter.text = GunManager.Instance._AmmoReserves.ToString();
        }
        else if (GunManager.Instance.currentlyEquipped != null)
        {
            _AmmoCounter.text = GunManager.Instance.currentlyEquipped.currentMagazine.ToString() + "/" + GunManager.Instance._AmmoReserves.ToString();
        }
    }
}
