using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public LayerMask mask;
    public float rayHeight = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        Quaternion startingAngle = Quaternion.AngleAxis(-45, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

        if (GunManager.Instance.currentGun == EGun.Torch)
        {
            DetectThings(startingAngle, stepAngle);
        }
        
    }

    void DetectThings(Quaternion start, Quaternion step)
    {
        RaycastHit hit;
        var angle = transform.rotation * start;
        var pos = transform.position;
        pos.y += rayHeight;
        var direction = angle * Vector3.forward;
        for (var i = 0; i < 24; i++)
        {
            Debug.DrawRay(pos, direction * 5, Color.red);
            if (Physics.Raycast(pos, direction, out hit, 5, mask))
            {
                if (hit.collider.gameObject.tag == "Invisible")
                {
                    Debug.Log("Visible");
                    //Enemy was seen
                    hit.collider.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                    hit.collider.gameObject.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    hit.collider.gameObject.tag = "Untagged";
                }

            }
            direction = step * direction;
        }


    }

    public Vector3 GetAngle(Vector3 origin, float angle)
    {
        return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
    }

}
