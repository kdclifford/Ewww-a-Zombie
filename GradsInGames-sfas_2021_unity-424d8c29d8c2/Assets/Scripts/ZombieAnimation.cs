using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private NavMeshAgent _NavMesh;
    private Animator _Animator;
    private GameObject target;
    private Health _Health;
    private float _Timer = 3;
    private float targetDistance = 0;
    private float targetNewDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
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
            targetDistance = targetNewDistance;
            targetNewDistance = Vector3.Distance(_NavMesh.destination, transform.position);
            //Debug.Log(targetNewDistance);

            _Animator.SetInteger("Attack", 0);
            if (targetNewDistance >= 0.1f)
            {
                _NavMesh.SetDestination(target.transform.position);
            }

            if (Vector3.Distance(transform.position, target.transform.position) > 1.51f)
            {
                _Animator.SetFloat("Velocity", Mathf.Abs(_NavMesh.velocity.x) + Mathf.Abs(_NavMesh.velocity.z));
            }
            else
            {
                _Animator.SetInteger("Attack", 1);
            }
        }
        else
        {
            //_NavMesh.ResetPath();
            _Animator.SetFloat("Velocity", 0f);
            Destroy(_NavMesh);
            ////Destroy(GetComponentInChildren<Collider>());            
            Destroy(transform.Find("Base HumanPelvis").GetComponent<CapsuleCollider>());
            transform.Find("Base HumanPelvis").Find("CollisionBox").gameObject.SetActive(false);
            SpawnManager.instance.ZombieDied();
            Destroy(this);
        }

        if(_Timer <= 0)
        {
            int random = Random.Range(0, 5);
            if(random == 1)
            {
                SoundManager.instance.Play(ESoundClipEnum.ZombieGrowl, gameObject);
            }
            _Timer = 5f;
        }


        _Timer -= Time.deltaTime;
    }
}
