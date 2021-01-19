using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Health>()._CurrentHealth <= 0)
        {
            gameOverUI.SetActive(true);
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}
