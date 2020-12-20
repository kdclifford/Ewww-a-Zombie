using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LowPowerTimer : MonoBehaviour
{
   private float timer = 0;
    private TMP_Text _timerText;
    private Animator _laptopAnimatior;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
        _laptopAnimatior = GameObject.FindGameObjectWithTag("Laptop").GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        timer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            UpdateTimer();
            UpdateText();
        }
    }

    void UpdateTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            PlayLaptopAnimation();
        }
    }

    void UpdateText()
    {
        _timerText.text = "Low Power: " + Mathf.Round(timer).ToString() + "%";
    }

    void PlayLaptopAnimation()
    {
        _laptopAnimatior.SetTrigger("CloseLid");
    }

}
