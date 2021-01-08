using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    //public GameObject[] _Lamps;
    //public Light[] _Lights;
    private ELight lightPos = ELight.None;
   
    public float startTime;

    private float _Timer = 0;
    public float timerInverse;
    private GameObject[] _lights;
    private Light[] _lightComp;
    private MeshRenderer[] _lightRenderer;

    public Color lightOn;
    public Color lightOff = new Color(0, 0, 0, 1);
    public Color newLightColour;

    public bool test = false;
    public bool test2 = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _lights = GameObject.FindGameObjectsWithTag("Lights");
        _lightComp = new Light[_lights.Length];
        _lightRenderer = new MeshRenderer[_lights.Length];

        for (int i = 0; i < _lights.Length; i++)
        {
            _lightComp[i] = _lights[i].transform.GetChild(0).GetComponent<Light>();
            _lightRenderer[i] = _lights[i].GetComponent<MeshRenderer>();
        }

        lightOn = _lightRenderer[0].material.GetColor("_EmissionColor");
    }

    public void InactiveLightsOn()
    {
        for (int i = 0; i < _lights.Length; i++)
        {

            if (_lightComp[i].enabled)
            {
                _lightRenderer[i].material.SetColor("_EmissionColor", Color.white);
                _lightComp[i].intensity = 1f;
                Debug.Log(_lightComp[i].intensity);
            }
        }
    }

    public void InactiveLightsOff()
    {
        for (int i = 0; i < _lightComp.Length; i++)
        {

            if (_lightComp[i].enabled)
            {
                _lightRenderer[i].material.SetColor("_EmissionColor", Color.black);
                _lightComp[i].intensity = 0f;
                Debug.Log(_lightComp[i].intensity);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
       // lightPos = ELight.None;
        _Timer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightPos == ELight.Off)
        {
            //InactiveLightsOff();
            if (_Timer >= 0)
            {
                timerInverse = 1.0f - (_Timer / startTime);
                newLightColour = Color.Lerp(lightOn, lightOff, timerInverse);
                LightsOff();
                updateTimer();
            }
        }

        else if(lightPos == ELight.On)
        {
            //InactiveLightsOn();
            if (_Timer >= 0)
            {
                timerInverse = 1.0f - (_Timer / startTime);
                newLightColour = newLightColour = Color.Lerp(lightOff, lightOn, timerInverse);
                LightsOn();
                updateTimer();
            }
        }

     if(test)
        {
            test = false;
            SetLightsOn();
        }

        if (test2)
        {
            test2 = false;
            SetLightsOff();
        }

    }

    void updateTimer()
    {
        if (_Timer > 0)
        {
            _Timer -= Time.deltaTime;
        }
    }


    void LightsOff()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lightRenderer[i].material.SetColor("_EmissionColor", newLightColour);
            _lightComp[i].intensity = 1 - timerInverse;
        }
        //lightPos = ELight.None;
    }

    void LightsOn()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lightRenderer[i].material.SetColor("_EmissionColor", newLightColour);
            _lightComp[i].intensity = timerInverse;
        }
        //lightPos = ELight.None;
    }

    public void SetLightsOn()
    {
        lightPos = ELight.On;
        _Timer = startTime;
    }

    public void SetLightsOff()
    {
        lightPos = ELight.Off;
        _Timer = startTime;
    }

}

public enum ELight
{
    On,
    Off,
    None
}