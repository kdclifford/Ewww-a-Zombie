using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private StoryData _data;

    private TextDisplay _output;
    private BeatData _currentBeat;
    private WaitForSeconds _wait;
    public bool active = false;
    private bool reset = false;

    private void Awake()
    {
        _output = GetComponentInChildren<TextDisplay>();
        _currentBeat = null;
        _wait = new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        if (active)
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
                    Application.Quit();
                }
                else
                {
                    DisplayBeat(1);
                }                
            }
            reset = false;
        }
        else
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
        _currentBeat = data;
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
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void Slot1Pistol()
    {
        GunController.Instance.SelectGun(ref GunManager.Instance._Gun1, EGun.Pistol);
        reset = true;
    }

    public void Slot1Rifle()
    {
        GunController.Instance.SelectGun(ref GunManager.Instance._Gun1, EGun.Rifle);
        reset = true;
    }

    public void Slot1Shotgun()
    {
        GunController.Instance.SelectGun(ref GunManager.Instance._Gun1, EGun.Shotgun);
        reset = true;
    }

    public void Slot2Pistol()
    {
        GunController.Instance.SelectGun(ref GunManager.Instance._Gun2, EGun.Pistol);
        reset = true;
    }

    public void Slot2Rifle()
    {
        GunController.Instance.SelectGun(ref GunManager.Instance._Gun2, EGun.Rifle);
        reset = true;
    }

    public void Slot2Shotgun()
    {
        GunController.Instance.SelectGun(ref GunManager.Instance._Gun2, EGun.Shotgun);
        reset = true;
    }

    public void TwoRandomGuns()
    {
        int newGun = Random.Range(6, 9);
        int newGun2 = Random.Range(6, 9);

        Debug.Log(newGun);
        Debug.Log(newGun2);

        while (newGun == newGun2)
        {
            newGun2 = Random.Range(6, 9);
        }

        GunManager.Instance.gunList[newGun].RandomStats();
        GunManager.Instance.gunList[newGun2].RandomStats();

        GunController.Instance.SelectGun(ref GunManager.Instance._Gun1, (EGun) newGun);
        GunController.Instance.SelectGun(ref GunManager.Instance._Gun2, (EGun)newGun2);

        reset = true;
    }



    public void ExitMenu()
    {
        CameraMovement.Instance.LaptopZoomOut();
        active = false;
    }


}
