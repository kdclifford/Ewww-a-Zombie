using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    private TMP_Text _Kills;
    private TMP_Text _Rounds;
    private TMP_Text _Points;

    // Start is called before the first frame update
    void Start()
    {
        _Kills = transform.GetChild(1).GetComponent<TMP_Text>();
        _Rounds = transform.GetChild(2).GetComponent<TMP_Text>();
        _Points = transform.GetChild(3).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _Kills.text = "Total Kills = " + SpawnManager.instance.overallZombieKills;
        _Rounds.text = "Rounds Survived = " + SpawnManager.instance.currentRound;
        _Points.text = "Total Points = " + SpawnManager.instance.overallPoints;
    }

}
