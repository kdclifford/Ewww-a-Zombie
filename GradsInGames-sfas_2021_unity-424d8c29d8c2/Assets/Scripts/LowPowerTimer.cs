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
    private GameObject[] _lights;
    private Light[] _lightComp;
    private MeshRenderer[] _lightRenderer;
    public Color lightOn;
    public Color lightOff = new Color(0, 0, 0, 1);
    public Color newLightColour;
    public float timerInverse;
    public Animator _CameraAnim;

    private void Awake()
    {
        _lights = GameObject.FindGameObjectsWithTag("Lights");
        _lightComp = new Light[_lights.Length];
        _lightRenderer = new MeshRenderer[_lights.Length];

        for (int i = 0; i < _lights.Length; i++)
        {
            _lightComp[i] = _lights[i].transform.GetChild(0).GetComponent<Light>();
            _lightRenderer[i] = _lights[i].GetComponent<MeshRenderer>();
        }

        lightOn = _lightRenderer[0].material.GetColor("_EmissionColor");
        _timerText = GetComponent<TMP_Text>();
        _laptopAnimatior = GameObject.FindGameObjectWithTag("Laptop").GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _Timer = startTime;
        _CameraAnim = Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Timer >= 0)
        {
        timerInverse = 1.0f - (_Timer / startTime);
            newLightColour = Color.Lerp(lightOn, lightOff, timerInverse);
            UpdateTimer();
            UpdateText();
            LightsOff();            
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
            CameraZoom();
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

    void LightsOff()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lightRenderer[i].material.SetColor("_EmissionColor", newLightColour);
            _lightComp[i].intensity = 1 - timerInverse;
        }
    }

    void CameraZoom()
    {
        _CameraAnim.SetTrigger("CameraZoomOut");
    }

    //void LightsOn()
    //{
    //    for (int i = 0; i < _lights.Length; i++)
    //    {
    //        _lightRenderer[i].material.SetColor("_EmissionColor", newLightColour);
    //        _lightComp[i].intensity = lightTimer;
    //    }
    //}

}
