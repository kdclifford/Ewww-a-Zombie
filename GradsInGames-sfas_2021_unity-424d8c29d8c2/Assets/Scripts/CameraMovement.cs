using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    public float _mouseSpeed;
    private float xRotation = 0f;
    public Vector3 startPos;
    private Vector3 topFloorPos;
    public float cameraHeight;
    private Vector3 originPos;
    private Quaternion originRot;

    private Vector3 laptopPos;
    private Quaternion laptopRot;

    private Vector3 tvPos;
    private Quaternion tvRot;

    private Vector3 computerPos;
    private Quaternion computerRot;

    private Vector3 newPos;
    private Quaternion newRot;

    public float testtime = 0;
    public float startTime = 10;
    public float timer = 0;
    private bool laptop = false;
    private bool tv = false;
    private bool computer = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originPos = transform.position;
        originRot = transform.rotation;

        startPos = transform.position - player.position;
        topFloorPos = startPos;

        laptopPos = new Vector3(4.68f, 1f, 1.56f);
        laptopRot = Quaternion.Euler( new Vector3(21.26f, -9.3f, 0f ));

        tvPos = new Vector3(-8.93f, -1.349f, -10f);
        tvRot = Quaternion.Euler(new Vector3(20.5f, 180f, 0f));

        computerPos = new Vector3(-10.68f, 1.66f, 9.48f);
        computerRot = Quaternion.Euler(new Vector3(20, 90, 0));

        timer = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey(KeyCode.T))
        //{
        //    SetOrigin();
        //    laptop = !laptop;
        //}

        if (Input.GetKey(KeyCode.P))
        {
            SetOrigin();
            computer = !computer;
        }

        if (laptop)
        {
            newPos = LerpPosition(originPos, laptopPos, testtime);
            newRot = LerpRotation(originRot, laptopRot, testtime);

            transform.position = newPos;
            transform.rotation = newRot;
            timer -= Time.deltaTime;
        }

        if (tv)
        {
            newPos = LerpPosition(originPos, tvPos, testtime);
            newRot = LerpRotation(originRot, tvRot, testtime);

            transform.position = newPos;
            transform.rotation = newRot;
            timer -= Time.deltaTime;
        }

       if (computer)
        {
            newPos = LerpPosition(originPos, computerPos, testtime);
            newRot = LerpRotation(originRot, computerRot, testtime);

            transform.position = newPos;
            transform.rotation = newRot;
            timer -= Time.deltaTime;
        }


        if (!laptop && !tv && !computer)
        {
            transform.position = new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z + startPos.z);
        }
       // startPos = transform.position - player.position;
    }

    public static Vector3 LerpPosition(Vector3 pos, Vector3 endPos, float time)
    {
       return pos = Vector3.Lerp(pos, endPos, time);              
    }

    public Quaternion LerpRotation(Quaternion rot, Quaternion endRot, float time)
    {
       return rot = Quaternion.Lerp(rot, endRot, time);
    }

    void SetOrigin()
    {

        originPos.x = player.position.x;
        originPos.z = player.position.z + startPos.z;
    }
}
