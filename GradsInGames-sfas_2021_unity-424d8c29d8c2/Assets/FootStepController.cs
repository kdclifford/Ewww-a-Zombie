using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepController : MonoBehaviour
{
    public GameObject rightFoot;
    public GameObject leftFoot;
    public GameObject rightPos;
    public GameObject leftPos;
    public Vector3 off;

    public void PlayRightFoot()
    {
        Quaternion newRot = Quaternion.Euler(new Vector3(0, rightPos.transform.rotation.eulerAngles.y, 0));
        Vector3 newPos = new Vector3(rightPos.transform.position.x , rightPos.transform.position.y, rightPos.transform.position.z + 0.5f);
        //rightFoot.Play();
        GameObject foot = MonoBehaviour.Instantiate(rightFoot, newPos, newRot);
    }

    public void PlayLeftFoot()
    {
        Vector3 newPos = new Vector3(rightPos.transform.position.x + off.x, rightPos.transform.position.y + off.y, rightPos.transform.position.z + off.z);
        Quaternion newRot = Quaternion.Euler(new Vector3(0, leftPos.transform.rotation.eulerAngles.y, 0));
        GameObject foot = MonoBehaviour.Instantiate(leftFoot, newPos, newRot);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
