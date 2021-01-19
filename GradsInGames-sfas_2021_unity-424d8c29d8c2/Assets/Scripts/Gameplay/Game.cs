using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private StoryData _data;

    private TextDisplay _output;
    private BeatData _currentBeat;
    private WaitForSeconds _wait;
    public bool _ScreenActive = false;
    private bool reset = false;
    private bool disable = false;
    private float statPrice = 20;

    private void Awake()
    {
        _output = GetComponentInChildren<TextDisplay>();
        _currentBeat = null;
        _wait = new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        if (_ScreenActive)
        {
            
            if (_output.IsIdle)
            {
                if (_currentBeat == null)
                {
                    DisplayBeat(1);
                }
                else
                {
                    UpdateInput();
                }
                //Debug.Log(_currentBeat.ID);
                //Debug.Log(_currentBeat.DisplayText);
            }
        }

    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || reset)
        {
            if (_currentBeat != null)
            {
                if (_currentBeat.ID == 1)
                {
                    DisplayBeat(1);
                }
                else
                {
                    DisplayBeat(1);
                }
            }
            reset = false;
        }
        else if (_currentBeat.Decision.Count > 0 && !disable)
        {
            KeyCode alpha = KeyCode.Alpha1;
            KeyCode keypad = KeyCode.Keypad1;

            for (int count = 0; count < _currentBeat.Decision.Count; ++count)
            {
                if (alpha <= KeyCode.Alpha9 && keypad <= KeyCode.Keypad9)
                {
                    if (Input.GetKeyDown(alpha) || Input.GetKeyDown(keypad))
                    {
                        ChoiceData choice = _currentBeat.Decision[count];

                        if (choice.GetChoiceType == ChoiceType.Function)
                        {
                            FunctionBeat(choice.NextID);
                            break;
                        }
                        else
                        {
                            DisplayBeat(choice.NextID);
                            break;
                        }
                    }
                }

                ++alpha;
                ++keypad;
            }
        }
    }


    private void FunctionBeat(int id)
    {
        BeatData data = _data.GetBeatById(id);
        SendMessage(data.DisplayText);
        // StartCoroutine(DoDisplay(data));
       // _currentBeat = data;
       // _output.Clear();
    }


    private void DisplayBeat(int id)
    {
        BeatData data = _data.GetBeatById(id);

        StartCoroutine(DoDisplay(data));
        _currentBeat = data;
    }

    private IEnumerator DoDisplay(BeatData data)
    {
        _output.Clear();

        while (_output.IsBusy)
        {
            yield return null;
        }

        _output.Display(data.DisplayText);

        while (_output.IsBusy)
        {
            yield return null;
        }

        for (int count = 0; count < data.Decision.Count; ++count)
        {
            ChoiceData choice = data.Decision[count];
            _output.Display(string.Format("{0}: {1}", (count + 1), choice.DisplayText));

            while (_output.IsBusy)
            {
                yield return null;
            }
        }

        if (data.Decision.Count > 0)
        {
            _output.ShowWaitingForInput();
        }
    }

    //Menu Functions

    public void StartGame()
    {
        _output.Clear();
       // transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        BeatData data = _data.GetBeatById(1);
        // data.Decision[0].DisplayText = "Resume Game?";
        // data = _data.GetBeatById(2);
        // data.DisplayText = "ResumeGame";
        DisableMenu();
       // reset = true;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        CameraMovement.Instance.LaptopZoomOut();
        reset = true;
    }

    public void Pistol()
    {
        if (SpawnManager.instance.currentPoints >= 100)
        {

            SpawnManager.instance.currentPoints -= 100;
            GunController.Instance.SelectGun(EGun.Pistol);
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Rifle()
    {
        if (SpawnManager.instance.currentPoints >= 150)
        {

            SpawnManager.instance.currentPoints -= 150;
            GunController.Instance.SelectGun(EGun.Rifle);
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Shotgun()
    {
        if (SpawnManager.instance.currentPoints >= 100)
        {

            SpawnManager.instance.currentPoints -= 100;
            GunController.Instance.SelectGun(EGun.Shotgun);
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void RandomGun()
    {
        if (SpawnManager.instance.currentPoints >= 300)
        {

            int newGun = Random.Range(6, 9);

            Debug.Log(newGun);

            GunManager.Instance.gunList[newGun].RandomStats();

            GunController.Instance.SelectGun((EGun)newGun);
            SpawnManager.instance.currentPoints -= 300;
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
       
    }

    public void MaxAmmo()
    {
        GunManager.Instance._AmmoReserves = GunManager.Instance._MAXAmmoReserves;
    }


    public void DisplayVolume()
    {
        UIController.Instance._VolumeSettings.SetActive(true);
        reset = true;
        DisableMenu();
    }

    public void DisplayConInfo()
    {
        UIController.Instance._ControllerInfo.SetActive(true);
        reset = true;
        DisableMenu();
    }

    public void ExitMenu()
    {
        CameraMovement.Instance.ComputerZoomOut();
        _ScreenActive = false;
        reset = true;
    }

    public void ExitLaptop()
    {
        CameraMovement.Instance.LaptopZoomOut();
        _ScreenActive = false;
        reset = true;
    }

    public void ExitTV()
    {
        CameraMovement.Instance.TVZoomOut();
        _ScreenActive = false;
        reset = true;
    }

    public void Firerate()
    {
        if (SpawnManager.instance.currentPoints >= statPrice)
        {
            if (GunManager.Instance.currentGun < EGun.AmountOfGuns)
            {
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].firerateUpgrade++;
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].fireRate = Mathf.Lerp(
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].StartFireRate,
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxFireRate,
                    ((float)GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].firerateUpgrade) / GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxUpgrades);
                SpawnManager.instance.currentPoints -= statPrice;
            }
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Damage()
    {
        if (SpawnManager.instance.currentPoints >= statPrice)
        {
            if (GunManager.Instance.currentGun < EGun.AmountOfGuns)
            {
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].damageUpgrade++;
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].damage = (int)Mathf.Lerp(
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].StartDamage,
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxDamage,
                    ((float)GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].damageUpgrade) / GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxUpgrades);
                SpawnManager.instance.currentPoints -= statPrice;
            }
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Crit()
    {
        if (SpawnManager.instance.currentPoints >= statPrice)
        {
            if (GunManager.Instance.currentGun < EGun.AmountOfGuns)
            {
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].critUpgrade++;
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].critChance = (int)Mathf.Lerp(
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].StartCritChance,
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxCritChance,
                    ((float)GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].critUpgrade) / GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxUpgrades);
                SpawnManager.instance.currentPoints -= statPrice;
            }
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Range()
    {
        if (SpawnManager.instance.currentPoints >= statPrice)
        {
            if (GunManager.Instance.currentGun < EGun.AmountOfGuns)
            {
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].rangeUpgrade++;
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].range = Mathf.Lerp(
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].StartRange,
                    GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxRange,
                    ((float)GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].rangeUpgrade) / GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxUpgrades);
                SpawnManager.instance.currentPoints -= statPrice;
            }
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Mag()
    {
        if (SpawnManager.instance.currentPoints >= statPrice)
        {
            if (GunManager.Instance.currentGun < EGun.AmountOfGuns)
            {
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].magUpgrade++;
                GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].fullMag = (int)Mathf.Lerp(
                     GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].startMagazine,
                     GunManager.Instance.gunList[(int)GunManager.Instance.currentGun].MaxMag,
                    ((float)GunManager.Instance.currentlyEquipped.magUpgrade) / 10);
                SpawnManager.instance.currentPoints -= statPrice;
            }
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Swap()
    {
        if (SpawnManager.instance.currentPoints >= statPrice && PlayerStats.Instance.AddSwap())
        {
            SpawnManager.instance.currentPoints -= statPrice;
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }


    public void Reload()
    {
        if (SpawnManager.instance.currentPoints >= statPrice && PlayerStats.Instance.AddReload())
        {
            SpawnManager.instance.currentPoints -= statPrice;
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Health()
    {
        if (SpawnManager.instance.currentPoints >= statPrice && PlayerStats.Instance.AddHealth())
        {
            SpawnManager.instance.currentPoints -= statPrice;
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    public void Movement()
    {
        if (SpawnManager.instance.currentPoints >= statPrice && PlayerStats.Instance.AddMovement())
        {
            SpawnManager.instance.currentPoints -= statPrice;
            reset = true;
        }
        else
        {
            NotEnoughPoints();
        }
    }

    void NotEnoughPoints()
    {
      
        DisplayBeat(10);
    }

    void ResetMenu()
    {
        reset = true;
    }

    public void ChangeStory(StoryData story)
    {
        _data = story;
        DisplayBeat(1);
        EnableMenu();
    }

    public void EnableMenu()
    {
        disable = false;
    }

    public void DisableMenu()
    {
        disable = true;
    }
}
