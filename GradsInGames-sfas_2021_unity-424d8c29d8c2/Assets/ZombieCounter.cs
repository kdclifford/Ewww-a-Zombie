using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieCounter : MonoBehaviour
{
    private TMP_Text _ZombieAmount;
    // Start is called before the first frame update
    void Start()
    {
        _ZombieAmount = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {        
            _ZombieAmount.text = "Zombie Amount:" + SpawnManager.instance.currentZombieAmount.ToString();       
    }
}
