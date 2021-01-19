using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private TMP_Text _Reload;
    private TMP_Text _Movement;
    private TMP_Text _Health;
    private TMP_Text _Swap;

    // Start is called before the first frame update
    void Start()
    {
        _Reload = transform.GetChild(1).GetComponent<TMP_Text>();
        _Movement = transform.GetChild(2).GetComponent<TMP_Text>();
        _Health = transform.GetChild(3).GetComponent<TMP_Text>();
        _Swap = transform.GetChild(4).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _Reload.text = "Reload Speed = " + PlayerStats.Instance.reloadMultiplier.ToString();
        _Health.text = "Max Health = " + PlayerStats.Instance.healthMultiplier.ToString();
        _Swap.text = "Weapon Swap Speed = " + PlayerStats.Instance.swapMultiplier.ToString();
        _Movement.text =  "Movement Speed = " + PlayerStats.Instance.movementMultiplier.ToString();
    }

}
