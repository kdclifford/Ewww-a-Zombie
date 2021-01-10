using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundCount : MonoBehaviour
{
    private TMP_Text _RoundCount;
    // Start is called before the first frame update
    void Start()
    {
        _RoundCount = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _RoundCount.text = "Round:" + SpawnManager.instance.currentRound.ToString();
    }
}
