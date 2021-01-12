using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LowPowerTimer : MonoBehaviour
{
    public float startTime;
    private float _Timer = 0;
    private TMP_Text _timerText;
    private Animator _laptopAnimatior;
    private CameraMovement _CameraMovement;
    //public Animator _CameraAnim;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
        _laptopAnimatior = GameObject.FindGameObjectWithTag("Laptop").GetComponent<Animator>();
        _CameraMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _Timer = startTime;
       // _CameraAnim = Camera.main.GetComponent<Animator>();
        LightManager.instance.SetLightsOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Timer >= 0)
        {            
            UpdateTimer();
            UpdateText();
        }
    

    }

    void UpdateTimer()
    {
        if (_Timer > 0)
        {
            _Timer -= Time.deltaTime;
        }

        if (_Timer <= 0)
        {
            PlayLaptopAnimation();
            _CameraMovement.LaptopZoomOut();
        }
    }

    void UpdateText()
    {
        _timerText.text = "Low Power: " + Mathf.Round(_Timer).ToString() + "%";
    }

    void PlayLaptopAnimation()
    {
        _laptopAnimatior.SetTrigger("CloseLid");
    }



    //void CameraZoom()
    //{
    //    _CameraAnim.SetTrigger("CameraZoomOut");
    //}

    //void LightsOn()
    //{
    //    for (int i = 0; i < _lights.Length; i++)
    //    {
    //        _lightRenderer[i].material.SetColor("_EmissionColor", newLightColour);
    //        _lightComp[i].intensity = lightTimer;
    //    }
    //}

}
