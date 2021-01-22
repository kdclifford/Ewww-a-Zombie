using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceInteraction : MonoBehaviour
{
    public Game laptop;
    public Game computer;
    public Game tv;
    public GameObject screenFade;
    public TMP_Text popup;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Laptop" && SpawnManager.instance._Start && !laptop._screenActive)
        {
            popup.text = "Press Space to Buy Stat Upgrades";
            if(Input.GetKey(KeyCode.Space) )
            {
                laptop._screenActive = true;
                CameraMovement.Instance.LaptopZoomIn();
            }
        }
        else if (other.gameObject.tag == "Computer" && SpawnManager.instance._Start && !computer._screenActive)
        {
            popup.text = "Press Space to Buy Guns";
            if (Input.GetKey(KeyCode.Space))
            {
                computer._screenActive = true;
                CameraMovement.Instance.ComputerZoomIn();
            }
        }
        else if (other.gameObject.tag == "TV" && SpawnManager.instance._Start && !tv._screenActive)
        {
            popup.text = "Press Space to Buy Gun Upgades";
            if (Input.GetKey(KeyCode.Space))
            {
                tv._screenActive = true;
                CameraMovement.Instance.TVZoomIn();
            }
        }
        else if (other.gameObject.tag == "ElectricBox")
        {
            popup.text = "Press Space to Turn On Electric";
            if (Input.GetKey(KeyCode.Space))
            {
                LightManager.instance.SetLightsOn();
                UIController.Instance.EneableUI();
                other.enabled = false;
                HintScript.Instance.AddHint(2);
                laptop._screenActive = false;
                screenFade.SetActive(false);
            }
        }
        else if (other.gameObject.tag == "PistolPickUP")
        {
            popup.text = "Press Space to Pick Up Pistol";
            if (Input.GetKey(KeyCode.Space))
            {
                other.gameObject.SetActive(false);
                GunController.Instance.SelectGun(EGun.Pistol);
                SpawnManager.instance._Start = true;
                HintScript.Instance.AddHint(3);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        popup.text = "";
    }

}
