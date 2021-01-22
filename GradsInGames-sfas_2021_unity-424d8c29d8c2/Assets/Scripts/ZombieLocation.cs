using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLocation : MonoBehaviour
{
    EAgentPos agentPos;
    private PlayerCurrentLocation playerPos;
    private SkinnedMeshRenderer _zombieMesh;
    private CapsuleCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerCurrentLocation>();
        _zombieMesh = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        _collider = transform.Find("Base HumanPelvis").GetComponent<CapsuleCollider>();
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

        if (_collider != null && _collider.tag != "Invisible")
        {
            if (agentPos != playerPos.playerPos)
            {
                _zombieMesh.enabled = (false);
            }
            else
            {
                _zombieMesh.enabled = (true);
            }
        }
    }

    public void DestoryOnDeath()
    {
        Destroy(gameObject, 5.0f);
    }

}
