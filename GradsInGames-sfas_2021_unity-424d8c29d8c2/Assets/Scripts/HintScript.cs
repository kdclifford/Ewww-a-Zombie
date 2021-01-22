using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintScript : MonoBehaviour
{
    TMP_Text hint;
    public int hintIndex = 0;
    private int oldHint = 0;


    private static HintScript _instance;

    public static HintScript Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        hint = GetComponent<TMP_Text>();
        hint.text = "Press T to Turn on Torch";
    }

    // Update is called once per frame
    void Update()
    {
        if (hintIndex == 0 && Input.GetKey(KeyCode.T))
        {
            hintIndex = 1;
        }

        if (oldHint != hintIndex)
        {
            HintDisplay();
            oldHint = hintIndex;
        }

    }

    public void AddHint(int hint)
    {
        hintIndex = hint;
    }


    public void HintDisplay()
    {
        switch (hintIndex)
        {
            case 1:
                hint.text = "Find the power Switch";
                return;
            case 2:
                hint.text = "Pick up the pistol off the Car";
                return;
            case 3:
                hint.text = "Survive";
                return;
        }

        hint.text = "";
    }
}
