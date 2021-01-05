using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int _MaxHealth = 10;

    public int _CurrentHealth;

    private Animator _Animator;
    bool dieOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        _CurrentHealth = _MaxHealth;
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_CurrentHealth <= 0 && !dieOnce)
        {
            _Animator.SetFloat("DeathDir", 1);
            _Animator.SetInteger("Die", 1);
            dieOnce = true;
        }
        else
        {
            _Animator.SetInteger("Die", 0);
        }
    }

    public void TakeDamage(int damage)
    {
        _CurrentHealth -= damage;
    }


}
