using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Health player;
    Image healthBar;
    Color startColour;
   public Color endColour;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthBar = transform.GetChild(0).GetComponent<Image>();
        startColour = healthBar.color;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = ((float)player._CurrentHealth) / player._MaxHealth;

        if( healthBar.fillAmount < 0.5f)
        {
            healthBar.color = Color.Lerp(endColour, startColour, healthBar.fillAmount * 2);
        }
    }
}
