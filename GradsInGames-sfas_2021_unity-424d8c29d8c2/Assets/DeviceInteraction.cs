﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceInteraction : MonoBehaviour
{
    public Game laptop;
    public Game computer;
    public Game tv;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Laptop")
        {
            if(Input.GetKey(KeyCode.Space) && !laptop._ScreenActive)
            {
                laptop._ScreenActive = true;
                CameraMovement.Instance.LaptopZoomIn();
            }
        }
        else if (other.gameObject.tag == "Computer" && !computer._ScreenActive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                computer._ScreenActive = true;
                CameraMovement.Instance.ComputerZoomIn();
            }
        }
        else if (other.gameObject.tag == "TV" && !tv._ScreenActive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                tv._ScreenActive = true;
                CameraMovement.Instance.TVZoomIn();
            }
        }
        else if (other.gameObject.tag == "ElectricBox")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                LightManager.instance.SetLightsOn();
                other.enabled = false;
            }
        }
        else if (other.gameObject.tag == "PistolPickUP")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                other.gameObject.SetActive(false);
                GunController.Instance.SelectGun(EGun.Pistol);
            }
        }
    }



}
