using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Vector3 newPos;
    Vector3 cameraRot;
    public float stencilHeight;
    public GameObject stencil;
    private CameraMovement _CameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        _CameraMovement = GetComponent<CameraMovement>();
        _CameraMovement.enabled = false;
        _CameraMovement.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = Camera.main.transform.position;
        newPos.y = stencilHeight;
        stencil.transform.position = newPos;
        cameraRot = Camera.main.transform.rotation.eulerAngles;
        stencil.transform.rotation = Quaternion.Euler(-cameraRot);
        //transform.rotation = Camera.main.transform.rotation;
    }
}
