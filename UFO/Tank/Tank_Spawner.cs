using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Spawner : MonoBehaviour
{

    private float spawnTimer = 0f;
    public float spawnTrigger = 10f;
    public bool isSpawning = false;
    private float spawnDelay = 0f;
    public GameObject tank_prefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Tiempo que espera el spawn para instanciar un nuevo tanque//
        if (isSpawning == false)
        {
            spawnTimer += 1 * Time.deltaTime;
        }

        
        //if (spawnTimer >= spawnTrigger && isSpawning == false)
        //{
            //Instantiate Tank//
        //    Instantiate(tank_prefab, gameObject.transform.position, gameObject.transform.rotation);
            //Reset timer//
        //    spawnTimer = 0f;
        //    isSpawning = true;
        //}

        //Tiempo que tarda el tanque en salir del spawn// //NO MODIFICAR//
        if (isSpawning == true)
        {
            spawnDelay += 1 * Time.deltaTime;
        }

        if (spawnDelay >= 8f)
        {
            isSpawning = false;
            spawnDelay = 0f;
        }

    }

    public void Spawn()
    {
        //Instantiate Tank//
        Instantiate(tank_prefab, gameObject.transform.position, gameObject.transform.rotation);
        //Reset timer//
        spawnTimer = 0f;
        isSpawning = true;
    }
}
