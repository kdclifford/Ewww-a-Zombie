using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    public float _MaxTimer = 3;
    private float _Timer = 0;
    private Health _Health;

    // Start is called before the first frame update
    void Start()
    {
        _Health = GetComponent<Health>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (_Health._CurrentHealth <= 0)
        {

        }
        else if (_Health._CurrentHealth < 100 && _Timer <= 0)
        {
            if (_Health._MaxHealth - 20 >= _Health._CurrentHealth)
            {
                _Health._CurrentHealth += 20;
            }
            else
            {
                _Health._CurrentHealth = _Health._MaxHealth;
            }

            _Timer = _MaxTimer;
        }

        _Timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            GetComponent<Health>()._CurrentHealth -= 6;
            _Timer = _MaxTimer;
        }
    }

}
