using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        Debug.DrawRay(transform.position, forward, Color.green);

        Quaternion rot = Quaternion.AngleAxis(45, Vector3.right);
        Vector3 world = transform.TransformDirection( rot * Vector3.forward);


        Debug.Log(GetAngle(transform.position, 45f));

        Debug.DrawRay(transform.position, transform.TransformDirection(GetAngle(transform.position, 45f)), Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(GetAngle(transform.position, -45f)), Color.red);
    }

      public Vector3 GetAngle(Vector3 origin, float angle)
    {
        return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
    }

}
