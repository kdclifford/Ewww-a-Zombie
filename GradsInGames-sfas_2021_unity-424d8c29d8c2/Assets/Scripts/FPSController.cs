using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public int targetFPS = 30;
    [SerializeField]
    private float actualFps;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetFPS != Application.targetFrameRate)
        {
            Application.targetFrameRate = targetFPS;
        }
        actualFps = Application.targetFrameRate;
    }
}
