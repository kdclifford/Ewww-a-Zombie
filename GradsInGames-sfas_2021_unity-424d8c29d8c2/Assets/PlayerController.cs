using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _movementSpeed;
    public Vector3 point;

    private Rigidbody _RB;
    private Animator _Animator;
    public GameObject aimPoint;
    public float hieght;
    // Start is called before the first frame update
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector2 walkRot = CurrentDirection(new Vector2(move.x, move.z), gameObject);

        _Animator.SetFloat("Xmove", walkRot.x);
        _Animator.SetFloat("Ymove", walkRot.y);

        //Get the Screen position of the mouse
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

       // Debug.Log(Input.mousePosition);

        float rayLength;

        if (ground.Raycast(cameraRay, out rayLength))
        {
            point = cameraRay.GetPoint(rayLength);
            point.y = hieght;
            Debug.DrawLine(cameraRay.origin, point, Color.red);
            transform.LookAt(new Vector3(point.x, transform.position.y + hieght, point.z));
            aimPoint.transform.position = point;
        }

        //Quaternion newRot = transform.rotation;
        //newRot.eulerAngles = new Vector3(0, transform.rotation.y, 0);
        //transform.rotation = newRot;
       // _RB.angularVelocity = Vector3.zero;

        move = move.normalized * _movementSpeed;

        //_RB.MovePosition(transform.position + Time.deltaTime * _movementSpeed * transform.TransformDirection(move));
        _RB.velocity = move;
    }


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
