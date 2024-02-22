using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumiSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnerPos;
    [SerializeField] GameObject lumiPrefab;
    [SerializeField] private float minSpawnTimer;
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private float current_spawnTimer;

    private void Start()
    {
        current_spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
    }

    private void Update()
    {
        current_spawnTimer -= Time.deltaTime;

        if (current_spawnTimer <= 0)
        {
            LumiSpawn();
            current_spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
        }
    }
    private void LumiSpawn()
    {
        transform.position = new Vector2(Random.Range(-1f, 8f), transform.position.y);
        Instantiate(lumiPrefab, transform.position, transform.rotation);
    }
}
