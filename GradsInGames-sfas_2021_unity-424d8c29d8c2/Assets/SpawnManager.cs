using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector]
    public int currentRound = 0;
    private int zombieMaxAmount = 3;
    private int zombieStartAmount = 3;
    [HideInInspector]
    public int currentZombieAmount = 0;
    private int currentlySpawned = 0;
    private int maxAmountOnMap = 10;

    public float starttimer = 30;
    [HideInInspector]
    public float roundtimer;
    private float zombieRoundMultiplier = 1.25f;
    private GameObject[] _Rooms;
    public GameObject zombiePrefab;
    public GameObject invisibleZombie;
    public static SpawnManager instance;
    private int specialRound = 5;

    [HideInInspector]
    public float currentPoints = 0;

    bool lights = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        roundtimer = starttimer;
        specialRound = Random.Range(4, 7);
        currentZombieAmount = zombieStartAmount;
        _Rooms = GameObject.FindGameObjectsWithTag("Room");
    }

    // Update is called once per frame
    void Update()
    {

        if (currentZombieAmount == 0 && currentRound != specialRound)
        {
            currentlySpawned = 0;
            currentRound++;
            zombieStartAmount = Mathf.RoundToInt((zombieMaxAmount + currentRound) * zombieRoundMultiplier);
            currentZombieAmount = zombieStartAmount;
            roundtimer = starttimer;
        }
        else if (currentZombieAmount == 0)
        {
            currentlySpawned = 0;
            currentRound++;
            zombieStartAmount = 5;
            currentZombieAmount = zombieStartAmount;
            roundtimer = starttimer;
            specialRound *= 2;
        }

        if (currentRound != specialRound)
        {
            if (lights)
            {
                LightManager.instance.SetLightsOn();
                lights = false;
            }

            if (currentlySpawned < currentZombieAmount && roundtimer <= 0)
            {
                if (currentlySpawned < maxAmountOnMap)
                {
                    SpawnZombies(zombiePrefab);
                }
            }
        }
        else
        {
            if (!lights)
            {
                lights = true;
                LightManager.instance.SetLightsOff();
            }

            if (currentlySpawned < currentZombieAmount && roundtimer <= 0)
            {
                if (currentlySpawned < maxAmountOnMap)
                {
                    SpawnZombies(invisibleZombie);
                }
            }
        }

        roundtimer -= Time.deltaTime;

    }

    void SpawnZombies(GameObject agent)
    {
        for (int i = 0; i < currentZombieAmount; i++)
        {
            if (currentlySpawned == maxAmountOnMap || currentlySpawned == currentZombieAmount)
            {
                break;
            }
            Debug.Log("Zombie Spawned");
            int rand = Random.Range(0, _Rooms.Length);
            GameObject zombie = MonoBehaviour.Instantiate(agent, _Rooms[rand].transform.position, Quaternion.identity);
            currentlySpawned++;

        }
    }

    public void ZombieDied()
    {
        currentlySpawned--;
        currentZombieAmount--;
        currentPoints += 10;
    }


}
