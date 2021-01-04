using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
   private GunManager gunManager;

    PlayerController playerController;

    Vector3 endPoint;
   public LayerMask layerMask;

    float timer = 0;

    public GameObject tracer;

    // Start is called before the first frame update
    void Start()
    {
        gunManager = GunManager.Instance;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (gunManager.currentGun != EGun.NoGun && gunManager.currentGun != EGun.FlashLight && Input.GetKey(KeyCode.Space) && timer < 0)
        {
            Vector3 newGunPos = transform.position;
            newGunPos.y = transform.position.y + gunManager.currentlyEquipped.gunHeight;
            if (Vector3.Distance(newGunPos, playerController.point) > 0.7f)
            {
                gunManager.currentlyEquipped.Fire(gunManager.particleSystem, tracer, playerController.point, layerMask);
                timer = gunManager.currentlyEquipped.fireRate;
            }
            else
            {
                gunManager.currentlyEquipped.Fire(gunManager.particleSystem, tracer, gunManager.particleSystem.transform.forward, layerMask);
                timer = gunManager.currentlyEquipped.fireRate;
            }
        }
        Debug.DrawRay(transform.position, (playerController.point - transform.position) * 10, Color.green);

        timer -= Time.deltaTime;
    }
}
