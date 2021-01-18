using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    public float _mouseSpeed;
    public Vector3 startPos;
    public float cameraHeight;
    private Vector3 originPos;
    private Quaternion originRot;

    private Vector3 laptopPos;
    private Quaternion laptopRot;
    private float _LaptopStartTime = 2;
    private float _LaptopTimer = 0;
    private bool _LaptopZoomIn = false;
    private bool _LaptopZoomOut = false;
    public GameObject laptopHud;
    public bool upgradeMenu = false;

    private Vector3 tvPos;
    private Quaternion tvRot;
    private float _TVStartTime = 2;
    private float _TVTimer = 0;
    private bool _TVZoomIn = false;
    private bool _TVZoomOut = false;
    public GameObject tvHud;

    private Vector3 computerPos;
    private Quaternion computerRot;
    private float _ComputerStartTime = 2;
    private float _ComputerTimer = 0;
    private bool _ComputerZoomIn = false;
    private bool _ComputerZoomOut = false;

    private Vector3 newPos;
    private Quaternion newRot;

    private bool isMenuOn = false;

    //public float testtime = 0;;

    private static CameraMovement _instance;

    public static CameraMovement Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originPos = transform.position;
        originRot = transform.rotation;

        startPos = transform.position - player.position;

        laptopPos = new Vector3(4.68f, 1f, 1.56f);
        laptopRot = Quaternion.Euler( new Vector3(21.26f, -9.3f, 0f ));

        tvPos = new Vector3(-8.93f, -1.349f, -10f);
        tvRot = Quaternion.Euler(new Vector3(20.5f, 180f, 0f));

        computerPos = new Vector3(-10.68f, 1.66f, 9.48f);
        computerRot = Quaternion.Euler(new Vector3(20, 90, 0));

        isMenuOn = true;

        // LaptopZoomIn();
        transform.position = laptopPos;
        transform.rotation = laptopRot;

    }

    // Update is called once per frame
    void Update()
    {
        if (_LaptopZoomIn)
        {
            ZoomIn(laptopPos, laptopRot, ref _LaptopTimer, _LaptopStartTime, ref _LaptopZoomIn);
            
        }
        else if (_LaptopZoomOut)
        {
            ZoomOut(laptopPos, laptopRot, ref _LaptopTimer, _LaptopStartTime,ref _LaptopZoomOut );
        }
        else if (_TVZoomIn)
        {
            ZoomIn(tvPos, tvRot, ref _TVTimer, _TVStartTime, ref _TVZoomIn);
        }
        else if (_TVZoomOut)
        {
            ZoomOut(tvPos, tvRot, ref _TVTimer, _TVStartTime, ref _TVZoomOut);
        }
        else if (_ComputerZoomIn)
        {
            ZoomIn(computerPos, computerRot, ref _ComputerTimer, _ComputerStartTime, ref _ComputerZoomIn);
        }
        else if (_ComputerZoomOut)
        {
            ZoomOut(computerPos, computerRot, ref _ComputerTimer, _ComputerStartTime, ref _ComputerZoomOut);
        }





     


        if (!isMenuOn)
        {
            transform.position = new Vector3(player.position.x, player.position.y + cameraHeight, player.position.z + startPos.z);
        }
        // startPos = transform.position - player.position;
    }

  public  void LaptopZoomIn()
    {
        isMenuOn = true;
        _LaptopTimer = _LaptopStartTime;
        SetOrigin();
        _LaptopZoomIn = true;
        _LaptopZoomOut = false;
     
            laptopHud.SetActive(true);
        
        upgradeMenu = true;
    }

  public  void LaptopZoomOut()
    {
        _LaptopTimer = _LaptopStartTime;
        //SetOrigin();
        _LaptopZoomIn = false;
        _LaptopZoomOut = true;
        laptopHud.SetActive(false);
    }


    public void TVZoomIn()
    {
        isMenuOn = true;
        _TVTimer = _TVStartTime;
        SetOrigin();
        _TVZoomIn = true;
        _TVZoomOut = false;
        tvHud.SetActive(true);
    }

    public void TVZoomOut()
    {
        _TVTimer = _TVStartTime;
        //SetOrigin();
        _TVZoomIn = false;
        _TVZoomOut = true;
        tvHud.SetActive(false);
    }

    public void ComputerZoomIn()
    {
        isMenuOn = true;
        _ComputerTimer = _ComputerStartTime;
        SetOrigin();
        _ComputerZoomIn = true;
        _ComputerZoomOut = false;
    }

    public void ComputerZoomOut()
    {
        _ComputerTimer = _ComputerStartTime;
        //SetOrigin();
        _ComputerZoomIn = false;
        _ComputerZoomOut = true;
    }



    private void ZoomIn(Vector3 pos, Quaternion rot, ref float timer, float timerStart, ref bool zoom)
    {
        newPos = LerpPosition(pos, originPos, timer / timerStart);
        newRot = LerpRotation(rot, originRot, timer / timerStart);

        transform.position = newPos;
        transform.rotation = newRot;
        if (timer < 0)
        {
            timer = 0;
            zoom = !zoom;
            player.GetComponent<PlayerController>().enabled = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void ZoomOut(Vector3 pos, Quaternion rot, ref float timer, float timerStart, ref bool zoom)
    {
        newPos = LerpPosition(originPos, pos, timer / timerStart);
        newRot = LerpRotation(originRot, rot, timer / timerStart);


        transform.position = newPos;
        transform.rotation = newRot;
        if (timer < 0)
        {
            timer = 0;
            isMenuOn = false;
            zoom = !zoom;
            player.GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
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
