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
        _Reload.text = PlayerStats.Instance.reloadMultiplier.ToString();
        _Health.text = PlayerStats.Instance.healthMultiplier.ToString();
        _Swap.text = PlayerStats.Instance.swapMultiplier.ToString();
        _Movement.text = PlayerStats.Instance.movementMultiplier.ToString();
    }

}
