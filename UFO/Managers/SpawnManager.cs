using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //PLAYER//

    
    // ROUNDS //
    public int roundNumber = 1;
    public int current_roundNumber = 1;
    private float current_spawnDelay = 0f;
    public float roundDelay;
    private float current_roundDelay;
    public static bool RoundFinished = false;



    // TANKS //
    public int enemyNumber;
    public int enemyCount;
    public int enemyKilled;
    public float spawnDelay;



    //ROCKETS//

    public int rocketNumber;
    public int rocketCount;
    public int rocketKilled;
    public float rocket_spawnDelay;
    public float current_rocket_spawnDelay;



    // night/day //
    public Light sun;
    public ambience_sounds playerCamera;



    // SPAWNERS //
    public List<GameObject> spawnList = new List<GameObject>();

    public List<GameObject> rocketList = new List<GameObject>();


    // Start is called before the first frame update
    private void Awake()
    {
        RoundFinished = false;

    }

    void Start()
    {
        spawnList.AddRange(GameObject.FindGameObjectsWithTag("Tank_Spawner"));
        rocketList.AddRange(GameObject.FindGameObjectsWithTag("Rocket_Spawner"));
        current_rocket_spawnDelay = rocket_spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {

        //Tank Spawn manager
        if (current_spawnDelay <= spawnDelay)
        {
            current_spawnDelay += Time.deltaTime;
        }


        if (current_spawnDelay >= spawnDelay && enemyCount < enemyNumber)
        {
            int spawnIndex = Random.Range(0, spawnList.Count);
            Tank_Spawner selected_spawn = spawnList[spawnIndex].GetComponent<Tank_Spawner>();

            if (selected_spawn.isSpawning == false)
            {
                selected_spawn.Spawn();
                enemyCount += 1;
                current_spawnDelay = 0;
            }
        }

        //Rocket Spawn manager

        


        if (RoundFinished == false && referenceManager.playerisalive == true)
        {
            
            current_rocket_spawnDelay -= Time.deltaTime;
            
            if (current_rocket_spawnDelay <= 0)
            {
                int spawnIndex = Random.Range(0, rocketList.Count);
                rocket_spawner selected_spawn = rocketList[spawnIndex].GetComponent<rocket_spawner>();

                selected_spawn.Spawn();
                rocketCount += 1;
                current_rocket_spawnDelay = rocket_spawnDelay;


            }
        }
        else
        {
            current_rocket_spawnDelay = rocket_spawnDelay;
        }


             
        //Round manager
        if (current_roundNumber < roundNumber && enemyNumber == enemyKilled)
        {
            current_roundDelay += 1 * Time.deltaTime;

            RoundFinished = true;
            if (current_roundDelay >= roundDelay)
            {
                current_roundNumber += 1;
                enemyCount = 0;
                enemyKilled = 0;
                enemyNumber += 2;
                current_roundDelay = 0;
                rocket_spawnDelay = rocket_spawnDelay - current_roundNumber * 0.2f;
                current_rocket_spawnDelay = rocket_spawnDelay;
                RoundFinished = false;
                playerCamera.RoundStart();
            }
        }
        


        //lights//
        if (RoundFinished == true && sun.intensity >= 0)
        {
            sun.intensity -= 0.5f * Time.deltaTime;

        }
        if (RoundFinished == false && sun.intensity <= 1)
        {
            sun.intensity += 0.5f * Time.deltaTime;

        }
    }
}
