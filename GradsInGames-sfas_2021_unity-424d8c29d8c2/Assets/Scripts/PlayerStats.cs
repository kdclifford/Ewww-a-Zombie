using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float movementMultiplier = 1;
    public float reloadMultiplier = 1;
    public float swapMultiplier = 1;
    public float healthMultiplier = 1;

    private float oldMovementMultiplier = 1;
    private float oldReloadMultiplier = 1;
    private float oldSwapMultiplier = 1;
    private float oldHealthMultiplier = 1;

    private int upgradeMax = 3;
    private int movementUpgrade = 0;
    private int healthUpgrade = 0;
    private int swapUpgrade = 0;
    private int reloadUpgrade = 0;

    private float increaseAmount = 0.25f;

    private Animator playerAnim;
    public Animator weaponAnim;
    private Health playerHealth;
    public PlayerController playerController;

    private static PlayerStats _instance;

    public static PlayerStats Instance { get { return _instance; } }


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

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        playerAnim.SetFloat("Multiplier", 1);
        weaponAnim.SetFloat("SwapMultiplier", 1);
    }

    private void Update()
    {
        if(oldMovementMultiplier != movementMultiplier && playerController.enabled == true)
        {
            playerAnim.SetFloat("Multiplier", movementMultiplier);
           // playerController._startMovementSpeed = playerController._startMovementSpeed * movementMultiplier;
            //Debug.Log
            oldSwapMultiplier = movementMultiplier;
        }

        if (oldReloadMultiplier != reloadMultiplier)
        {
            for (int i = 0; i < GunManager.Instance.gunList.Count; i++)
            {
                if(GunManager.Instance.gunList[i].gunType != EGun.NoGun && GunManager.Instance.gunList[i].gunType != EGun.Torch)
                {
                    GunManager.Instance.gunList[i].reloadSpeed /= reloadMultiplier;
                }
            }
            oldReloadMultiplier = reloadMultiplier;
        }

        if (oldSwapMultiplier != swapMultiplier)
        {
            weaponAnim.SetFloat("SwapMultiplier", swapMultiplier);
            oldSwapMultiplier = swapMultiplier;
        }

        if(oldHealthMultiplier != healthMultiplier)
        {
            playerHealth._MaxHealth = (int)(playerHealth._MaxHealth * healthMultiplier);
            oldHealthMultiplier = healthMultiplier;
        }

    }

    public bool AddMovement()
    {
        if(movementUpgrade < upgradeMax)
        {
        movementMultiplier += increaseAmount;
            return true;
        }
        return false;
    }

    public bool AddReload()
    {
        if (reloadUpgrade < upgradeMax)
        {
            reloadMultiplier += increaseAmount;
            return true;
        }
        return false;
    }

    public bool AddSwap()
    {
        if (swapUpgrade < upgradeMax)
        {
            swapMultiplier += increaseAmount;
            return true;
        }
        return false;
    }
    public bool AddHealth()
    {
        if (healthUpgrade < upgradeMax)
        {
            healthMultiplier += increaseAmount;
            return true;
        }
        return false;
    }
}
