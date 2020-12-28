using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    public Vector3 _CamPos;
    public Vector3 _CamRot;
    private Quaternion _CamQuat;

    // Start is called before the first frame update
    void Start()
    {
        _CamQuat = Quaternion.Euler(_CamRot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveAnimator()
    {
        Destroy(GetComponent<Animator>());
    }

}
