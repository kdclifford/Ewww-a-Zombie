using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float _mouseSpeed;
    private float xRotation = 0f;
    public Vector3 startPos;
    private Vector3 topFloorPos;
    public float cameraHeight;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position - player.position;
        topFloorPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
       // startPos = transform.position - player.position;
        transform.position = new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z + startPos.z);
    }
}
