using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _movementSpeed;
    private Rigidbody _RB;
    // Start is called before the first frame update
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //Get the Screen position of the mouse
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (ground.Raycast(cameraRay, out rayLength))
        {
            Vector3 point = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, point, Color.red);
            transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
           
        }

        //Quaternion newRot = transform.rotation;
        //newRot.eulerAngles = new Vector3(0, transform.rotation.y, 0);
        //transform.rotation = newRot;


        move = move.normalized * _movementSpeed * Time.deltaTime;

        //_RB.MovePosition(transform.position + Time.deltaTime * _movementSpeed * transform.TransformDirection(move));
        _RB.velocity = move;
    }
}
