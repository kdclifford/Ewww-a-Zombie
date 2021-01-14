﻿using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private StoryData _data;

    private TextDisplay _output;
    private BeatData _currentBeat;
    private WaitForSeconds _wait;
    public bool _ScreenActive = false;
    private bool reset = false;

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
        BeatData data = _data.GetBeatById(1);
       // data.Decision[0].DisplayText = "Resume Game?";
        data = _data.GetBeatById(2);
        data.DisplayText = "ResumeGame";

        reset = true;
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
        GunController.Instance.SelectGun(EGun.Pistol);
        reset = true;
    }

    public void Rifle()
    {
        GunController.Instance.SelectGun(EGun.Rifle);
        reset = true;
    }

    public void Shotgun()
    {
        GunController.Instance.SelectGun(EGun.Shotgun);
        reset = true;
    }

    public void RandomGun()
    {
        int newGun = Random.Range(6, 9);

        Debug.Log(newGun);

        GunManager.Instance.gunList[newGun].RandomStats();

        GunController.Instance.SelectGun((EGun) newGun);

        reset = true;
    }

    public void DisplayVolume()
    {
        UIController.Instance._VolumeSettings.SetActive(true);
        reset = true;
    }

    public void DisplayConInfo()
    {
        UIController.Instance._ControllerInfo.SetActive(true);
        reset = true;
    }

    public void ExitMenu()
    {
        CameraMovement.Instance.ComputerZoomOut();
        _ScreenActive = false;
        reset = true;
    }


}
