using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    Rigidbody _RB;
    // Start is called before the first frame update
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_RB.velocity == Vector3.zero)
        {
            Destroy(gameObject);
        }

    }
}
