using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Text _RoundCount;
    public TMP_Text _RoundTimer;
    public TMP_Text _ZombieAmount;
    public TMP_Text _AmmoCounter;
    public TMP_Text _PointCount;
    public GameObject _VolumeSettings;
    public GameObject _ControllerInfo;

    private static UIController _instance;

    public static UIController Instance { get { return _instance; } }


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

    // Update is called once per frame
    void Update()
    {
        _PointCount.text = "Points:" + SpawnManager.instance.currentPoints.ToString();
        _RoundCount.text = "Round:" + SpawnManager.instance.currentRound.ToString();
        if (SpawnManager.instance.roundtimer >= 0)
        {
            _RoundTimer.text = "Starts in:" + (int)SpawnManager.instance.roundtimer;
        }
        _ZombieAmount.text = "Zombies Left:" + SpawnManager.instance.currentZombieAmount.ToString();

        if (GunManager.Instance.currentGun == EGun.NoGun || GunManager.Instance.currentGun == EGun.FlashLight)
        {
            _AmmoCounter.text = GunManager.Instance._AmmoReserves.ToString();
        }
        else if (GunManager.Instance.currentlyEquipped != null)
        {
            if (GunManager.Instance.currentlyEquipped.currentMagazine == 0)
            {
                _AmmoCounter.text = "Realoading /" + GunManager.Instance._AmmoReserves.ToString();
            }
            else
            {
                _AmmoCounter.text = GunManager.Instance.currentlyEquipped.currentMagazine.ToString() + "/" + GunManager.Instance._AmmoReserves.ToString();
            }
        }
    }
}
