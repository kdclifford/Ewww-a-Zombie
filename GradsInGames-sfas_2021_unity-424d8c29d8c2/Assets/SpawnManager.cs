using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int currentRound = 1;
    private int zombieMaxAmount = 3;
    private int zombieStartAmount = 3;
    [HideInInspector]
    public int currentZombieAmount = 0;
    private int currentlySpawned = 0;
    private int maxAmountOnMap = 10;


    private float zombieRoundMultiplier = 1.25f;
    private GameObject[] _Rooms;
    public GameObject zombiePrefab;
    public static SpawnManager instance;

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
        currentZombieAmount = zombieStartAmount;
        _Rooms = GameObject.FindGameObjectsWithTag("Room");
        SpawnZombies();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentZombieAmount == 0)
        {
            currentlySpawned = 0;
            currentRound++;
            zombieStartAmount = Mathf.RoundToInt( (zombieMaxAmount + currentRound) * zombieRoundMultiplier);
            currentZombieAmount = zombieStartAmount;           
        }

        if (currentlySpawned < currentZombieAmount)
        {
            if (currentlySpawned < maxAmountOnMap)
            {
                SpawnZombies();
            }
        }
    }

    void SpawnZombies()
    {
        for (int i = 0; i < currentZombieAmount; i++)
        {
            if(currentlySpawned == maxAmountOnMap || currentlySpawned == currentZombieAmount)
            {
                break;
            }
            Debug.Log("Zombie Spawned");
            int rand = Random.Range(0, _Rooms.Length);
            GameObject zombie = MonoBehaviour.Instantiate(zombiePrefab, _Rooms[rand].transform.position , Quaternion.identity);
            currentlySpawned++;

        }
    }

    public void ZombieDied()
    {
        currentlySpawned--;
        currentZombieAmount--;
    }


}
