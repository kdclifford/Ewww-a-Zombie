using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private NavMeshAgent _NavMesh;
    private Animator _Animator;
    public GameObject target;
    private Health _Health;

    // Start is called before the first frame update
    void Start()
    {
        _NavMesh = GetComponent<NavMeshAgent>();
        _Animator = GetComponent<Animator>();
        _NavMesh.SetDestination(target.transform.position);
        _Health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Health._CurrentHealth > 0)
        {
            if (_NavMesh.destination != target.transform.position)
            {
                _NavMesh.SetDestination(target.transform.position);
            }

            if (Vector3.Distance(transform.position, target.transform.position) > 1.51f)
            {
                _Animator.SetFloat("Velocity", Mathf.Abs(_NavMesh.velocity.x) + Mathf.Abs(_NavMesh.velocity.z));
            }
            else
            {
                _Animator.Play("Z_Attack");
            }
        }
        else
        {
            _NavMesh.ResetPath();
            _Animator.SetFloat("Velocity", 0f);
            Destroy(_NavMesh);
            Destroy(GetComponentInChildren<Collider>());
            Destroy(this);
        }
    }
}
