using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private float minSpawnTimer;
    [SerializeField] private float current_spawnTimer;

    private bool isActivated;


    private void OnEnable()
    {
        Actions.OnDay += DeactivateSpawn;
        Actions.OnNight += ActivateSpawn;
    }
    private void Start()
    {
        current_spawnTimer = GenerateRandomTime();
    }

    private void Update()
    {
        if (isActivated)
        {
            if (current_spawnTimer > 0)
            {
                current_spawnTimer -= Time.deltaTime;
            }
            else
            {
                Actions.OnEnemySpawn();
                current_spawnTimer = GenerateRandomTime();
            }
        }
        
    }

    private float GenerateRandomTime()
    {
        float spawnTime = Random.Range(minSpawnTimer, maxSpawnTimer);
        return(spawnTime);
    }

    private void DeactivateSpawn()
    {
        isActivated = false;
        current_spawnTimer = GenerateRandomTime();
    }

    private void ActivateSpawn()
    {
        isActivated = true;
    }
}
