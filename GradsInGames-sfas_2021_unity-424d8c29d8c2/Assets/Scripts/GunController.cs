using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Rifle rifle = new Rifle();
    Shotgun shotgun = new Shotgun();
    PlayerController playerController;
    public ParticleSystem particleSystem;

    Vector3 endPoint;
   public LayerMask layerMask;

    float timer;

    public GameObject tracer;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        timer = rifle.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && timer < 0)
        {
            shotgun.Fire(particleSystem, tracer, playerController.point, layerMask);
            timer = rifle.fireRate;
        }
       // Debug.DrawRay(transform.position, (playerController.point - transform.position) * 10, Color.green);

        timer -= Time.deltaTime;
    }



}
