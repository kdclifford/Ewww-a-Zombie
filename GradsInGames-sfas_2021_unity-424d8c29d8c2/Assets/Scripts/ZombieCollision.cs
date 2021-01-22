using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCollision : MonoBehaviour
{
    public NavMeshAgent navMesh;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.GetMask("Player"))
        {
            navMesh.velocity = Vector3.zero;
        }
    }
}
