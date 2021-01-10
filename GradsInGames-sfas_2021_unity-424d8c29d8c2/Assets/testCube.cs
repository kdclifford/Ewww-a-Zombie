using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCube : MonoBehaviour
{
    EAgentPos agentPos;
    public SkinnedMeshRenderer cube;
    private PlayerCurrentLocation playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerCurrentLocation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > -2.4f)
        {
            agentPos = EAgentPos.Upstairs;
        }
        else
        {
            agentPos = EAgentPos.Downstairs;
        }

        if(agentPos != playerPos.playerPos)
        {
            cube.enabled = (false);
        }
        else
        {
            cube.enabled = (true);
        }


    }
}
