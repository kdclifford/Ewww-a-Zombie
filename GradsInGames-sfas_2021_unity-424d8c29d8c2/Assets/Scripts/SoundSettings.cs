using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;

public class SoundSettings : MonoBehaviour
{
    float volMax = 10;
    float volMin = 0.001f;
    float masterVol = 10;
    float gunVol = 10;
    float zombieVol = 10;
    float miscVol = 10;

    private AudioMixer mixer;

    public TMP_Text masterText;
    public TMP_Text gunText;
    public TMP_Text zombieText;
    public TMP_Text miscText;

    private void Start()
    {
        mixer = Resources.Load("Sounds/MasterMixer") as AudioMixer;
        masterText.text = (masterVol).ToString();
        gunText.text = (gunVol).ToString();
        zombieText.text = (zombieVol).ToString();
        miscText.text = (miscVol).ToString();
    }


    public void MasterVolumeUp()
    {
        if (masterVol < volMax)
        {
            masterVol++;
            masterVol = VolumeMax(masterVol);
            mixer.SetFloat("Master", Mathf.Log10(masterVol / volMax) * 20);
            masterText.text = ((int)masterVol).ToString();
        }
    }

    public void MasterVolumeDown()
    {
        if (masterVol > volMin)
        {
            masterVol--;
            masterVol = VolumeMin(masterVol);
            mixer.SetFloat("Master", Mathf.Log10(masterVol / volMax) * 20);
            masterText.text = ((int)masterVol).ToString();
        }
    }

    public void ZombieVolumeUp()
    {
        if (zombieVol < volMax)
        {
            zombieVol++;
            zombieVol = VolumeMax(zombieVol);
            mixer.SetFloat("Zombie", Mathf.Log10(zombieVol / volMax) * 20);
            zombieText.text = ((int)zombieVol).ToString();
        }
    }

    public void ZombieVolumeDown()
    {
        if (zombieVol > volMin)
        {
            zombieVol--;
            zombieVol = VolumeMin(zombieVol);
            mixer.SetFloat("Zombie", Mathf.Log10( zombieVol / volMax) * 20);
            zombieText.text = ((int)zombieVol).ToString();
        }
    }

    public void GunVolumeUp()
    {
        if (gunVol < volMax)
        {
            gunVol++;
            gunVol = VolumeMax(gunVol);
            mixer.SetFloat("Gun", Mathf.Log10(gunVol / volMax) * 20);
            gunText.text = ((int)gunVol).ToString();
        }
    }

    public void GunVolumeDown()
    {
        if (gunVol > volMin)
        {
            gunVol--;
            gunVol = VolumeMin(gunVol);
            mixer.SetFloat("Gun", Mathf.Log10(gunVol / volMax) * 20);
            gunText.text = ((int)gunVol).ToString();
        }
    }

    public void MiscVolumeUp()
    {
        if (miscVol < volMax)
        {
            miscVol++;
            miscVol = VolumeMax(miscVol);
            mixer.SetFloat("Misc", Mathf.Log10(miscVol / volMax) * 20);
            miscText.text = ((int)miscVol).ToString();
        }
    }

    public void MiscVolumeDown()
    {
        if (miscVol > volMin)
        {
            miscVol--;
            miscVol = VolumeMin(miscVol);
            mixer.SetFloat("Misc", Mathf.Log10(miscVol / volMax) * 20);
            miscText.text = ((int)miscVol).ToString();
        }
    }

    //private void Update()
    //{
    //   Debug.Log( Mathf.Log10(miscVol / volMax) * 20);
    //}

    float VolumeMax(float vol)
    {
        if (vol > volMax)
        {
            return volMax;
        }
        return vol;
    }

    float VolumeMin(float vol)
    {
        if (vol < volMin)
        {
            return volMin;
        }
        return vol;
    }

}
