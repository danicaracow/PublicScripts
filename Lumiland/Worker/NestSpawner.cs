using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestSpawner : MonoBehaviour
{
    [SerializeField] GameObject workerPrefab;
    [SerializeField] GameObject soldierPrefab;

    private void OnEnable()
    {
        Actions.OnWorkerSpawn += SpawnWorker;
        Actions.OnSoldierSpawn += SpawnSoldier;
    }

    private void SpawnWorker()
    {
        Instantiate(workerPrefab, transform.position, transform.rotation);
    }

    private void SpawnSoldier()
    {
        Instantiate(soldierPrefab, transform.position, transform.rotation);
    }

    
}
