using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNestSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;

    private void OnEnable()
    {
        Actions.OnEnemySpawn += SpawnEnemy;
    }

    private void SpawnEnemy()
    {
        transform.position = new Vector2(Random.Range(xMin, xMax), transform.position.y);
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
