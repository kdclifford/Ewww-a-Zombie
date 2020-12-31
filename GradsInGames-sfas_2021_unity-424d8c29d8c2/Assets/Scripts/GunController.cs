using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Rifle rifle = new Rifle();
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
            var bullet = Instantiate(tracer, particleSystem.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = (playerController.point - transform.position).normalized * 100;
            Shoot(ref bullet);
            timer = rifle.fireRate;
        }
        Debug.DrawRay(transform.position, (playerController.point - transform.position) * 10, Color.green);

        timer -= Time.deltaTime;
    }

    void Shoot(ref GameObject tracer)
    {
        

        particleSystem.Play();
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerController.point - transform.position, out hit, rifle.range, layerMask))
        {
            Debug.Log(hit.transform.name);
            tracer.transform.Translate(Vector3.forward);
        }
    }

}
