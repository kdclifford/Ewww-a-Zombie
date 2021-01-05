using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    Rigidbody _RB;
   public LayerMask[] hitLayers;

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

    private void OnTriggerEnter(Collider other)
    {
        //foreach (LayerMask mask in hitLayers)
        
            if (other.gameObject.layer != 11 && other.gameObject.layer != 12 && other.gameObject.layer != 15)
            {
                Debug.Log(other.name);
                Destroy(gameObject);
            }
        
    }

}
