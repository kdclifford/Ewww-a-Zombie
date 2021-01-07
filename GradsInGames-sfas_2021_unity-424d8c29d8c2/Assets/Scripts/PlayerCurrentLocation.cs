using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentLocation : MonoBehaviour
{
    public GameObject upstairs;
    public GameObject downstairs;
    public GameObject stencilObject;
    public EAgentPos playerPos;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > -2.5f)
        {
            playerPos = EAgentPos.Upstairs;
            downstairs.SetActive(false);
            upstairs.SetActive(true);
        }
        else
        {
            playerPos = EAgentPos.Downstairs;
            downstairs.SetActive(true);
            upstairs.SetActive(false);
        }
    }
}

public enum EAgentPos
{
    Upstairs,
    Downstairs
}