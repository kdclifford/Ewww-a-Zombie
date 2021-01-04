using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _movementSpeed;
    public Vector3 point;

    //private Rigidbody _RB;
    private Animator _Animator;
    // public GameObject aimPoint;
    // public float hieght;
    public CharacterController controller;
    Vector3 velocity;
    float gravity = -9.81f;

    bool isGrounded;
   // public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    // Start is called before the first frame update
    void Start()
    {
        //_RB = GetComponent<Rigidbody>();
        _Animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       // isGrounded = Physics.CheckSphere(transform.position, groundDistance, ~groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector2 walkRot = CurrentDirection(new Vector2(move.x, move.z), gameObject);

        _Animator.SetFloat("Xmove", walkRot.x);
        _Animator.SetFloat("Ymove", walkRot.y);

        //Get the Screen position of the mouse
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition );
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        // Debug.Log(Input.mousePosition);

        Vector3 newGunPos = transform.position;
        newGunPos.y = transform.position.y + GunManager.Instance.currentlyEquipped.gunHeight;

        Plane groundPlane = new Plane(Vector3.up, newGunPos);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        Vector3 axis = Vector3.zero;

        if (groundPlane.Raycast(ray, out distance))
        {
            point = ray.GetPoint(distance);
            axis = (point - newGunPos).normalized;
            axis = new Vector3(axis.x, 0f, axis.z);
            if (Vector3.Distance(point , newGunPos) > 0.5f)
            {
                transform.rotation = Quaternion.LookRotation(axis);
            }
        }



        //float rayLength;
        //RaycastHit hit;
        //if (Physics.Raycast(cameraRay, out hit, 1000, groundMask))
        //{
        //    point = hit.point;
        //    //point.y = hieght;
        //    Debug.DrawLine(cameraRay.origin, point - cameraRay.direction, Color.red);
        //    if (Vector3.Distance(new Vector3(point.x, transform.position.y, point.z), transform.position) > 0.5f)
        //    {
        //        transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
        //    }
        //   // aimPoint.transform.position = point;
        //}

        //Quaternion newRot = transform.rotation;
        //newRot.eulerAngles = new Vector3(0, transform.rotation.y, 0);
        //transform.rotation = newRot;
       // _RB.angularVelocity = Vector3.zero;

        move = move.normalized * (_movementSpeed * Time.deltaTime);

        //_RB.MovePosition(transform.position + Time.deltaTime * _movementSpeed * transform.TransformDirection(move));
        // _RB.velocity = move;

        controller.Move(move);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    //private void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down + transform.position, out hit, 1))
    //    {
    //        if (hit.transform.gameObject.layer == LayerMask.GetMask("Stair"))
    //        {
    //            _RB.velocity = new Vector3(_RB.velocity.x, 0, _RB.velocity.z);
    //        }
    //    }
    //}


    public static Vector2 CurrentDirection(Vector2 input, GameObject agent)
    {
        var a = Vector3.SignedAngle(new Vector3(input.x, 0, input.y), agent.transform.forward, agent.transform.up);

        // Normalize the angle
        if (a < 0)
        {
            a *= -1;
        }
        else
        {
            a = 360 - a;
        }

        // Take into consideration the angle of the camera
        //a += Camera.main.transform.eulerAngles.y;

        var aRad = Mathf.Deg2Rad * a; // degrees to radians
                                      //Debug.Log(leftStickInputAxis);
                                      // If there is some form of input, calculate the new axis relative to the rotation of the model
        if (input.x != 0 || input.y != 0)
        {
            input = new Vector2(Mathf.Sin(aRad), Mathf.Cos(aRad));
        }


        if (input.x < -1)
        {
            input.x = -1;
        }
        else if (input.x > 1.6f)
        {
            input.x = 1.6f;
        }


        if (input.y < -1)
        {
            input.y = -1;
        }
        else if (input.y > 1.6f)
        {
            input.y = 1.6f;
        }

        return input;
    }

}
