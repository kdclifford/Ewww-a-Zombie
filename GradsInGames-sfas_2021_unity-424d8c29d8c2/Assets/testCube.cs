using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCube : MonoBehaviour
{
    EAgentPos agentPos;
    public GameObject cube;
    public PlayerCurrentLocation playerPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > -2.5f)
        {
            agentPos = EAgentPos.Upstairs;
        }
        else
        {
            agentPos = EAgentPos.Downstairs;
        }

        if(agentPos != playerPos.playerPos)
        {
            cube.SetActive(false);
        }
        else
        {
            cube.SetActive(true);
        }


    }
}
