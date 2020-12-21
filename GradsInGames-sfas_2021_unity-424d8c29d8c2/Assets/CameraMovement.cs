using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float _mouseSpeed;
    private float xRotation = 0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, startPos.y, player.position.z + startPos.z);
    }
}
