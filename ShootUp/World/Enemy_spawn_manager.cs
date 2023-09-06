using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy_spawn_manager : MonoBehaviour
{
    //Spawners//
    public GameObject topLeftSpawner;
    public GameObject topRightSpawner;
    public GameObject midLeftSpawner;
    public GameObject midRightSpawner;

    //Prefabs//
    public GameObject planePrefab;
    public GameObject smallPlanePrefab;
    public GameObject allyPlanePrefab;

    //Plane number and Timers//
    public int maximumPlanes;
    public int maximumSmallPlanes;
    public int maximumAllyPlanes;
    public int current_planes;
    public int current_smallPlanes;
    public int current_allyPlanes;
    public float spawnTimePlanes;
    public float spawnTimeSmallPlanes;
    public float spawnTimeAllyPlanes;
    [SerializeField]
    private float current_spawnTimePlanes;
    [SerializeField]
    private float current_spawnTimeSmallPlanes;
    [SerializeField]
    private float current_spawnTimeAllyPlanes;




    // Start is called before the first frame update
    void Start()
    {
        current_spawnTimePlanes = spawnTimePlanes + Random.Range(-2f, 2f);
        current_spawnTimeSmallPlanes = spawnTimeSmallPlanes + Random.Range(-1f, 1f);
        current_spawnTimeAllyPlanes = spawnTimeAllyPlanes + Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {


        if (current_spawnTimePlanes > 0)
        {
            current_spawnTimePlanes -= Time.deltaTime;
        }

        if (current_spawnTimeSmallPlanes > 0)
        {
            current_spawnTimeSmallPlanes -= Time.deltaTime;
        }

        if (current_spawnTimeAllyPlanes > 0)
        {
            current_spawnTimeAllyPlanes -= Time.deltaTime;
        }

        if (current_spawnTimePlanes <= 0 && maximumPlanes > current_planes)
        {
            SpawnPlane();
        }

        if (current_spawnTimeSmallPlanes <= 0 && maximumSmallPlanes > current_smallPlanes)
        {
            SpawnSmallPlane();
        }

        if (current_spawnTimeAllyPlanes <= 0 && maximumAllyPlanes > current_allyPlanes)
        {
            SpawnAllyPlane();
        }

    }

    public void SpawnPlane()
    {
        var randomSpawner = Random.Range(0, 2);
        if (randomSpawner == 0)
        {
            Instantiate(planePrefab, topLeftSpawner.transform.position, topLeftSpawner.transform.rotation);
        }
        else
        {
            GameObject enemy_instace = Instantiate(planePrefab, topRightSpawner.transform.position, topRightSpawner.transform.rotation);
            enemy_instace.GetComponent<Enemy_variables>().speed = enemy_instace.GetComponent<Enemy_variables>().speed * -1;
        }
        current_planes += 1;
        current_spawnTimePlanes = spawnTimePlanes + Random.Range(-3f, 3f);
        
    }

    public void SpawnSmallPlane()
    {
        var randomSpawner = Random.Range(0, 2);
        if (randomSpawner == 0)
        {
            Instantiate(smallPlanePrefab, midLeftSpawner.transform.position * new Vector2(1, Random.Range(-0.5f, 0.5f)), midLeftSpawner.transform.rotation);
        }
        else
        {
            GameObject enemy_instace = Instantiate(smallPlanePrefab, midRightSpawner.transform.position * new Vector2(1, Random.Range(-0.5f, 0.5f)), midRightSpawner.transform.rotation);
            enemy_instace.GetComponent<Enemy_variables>().speed = enemy_instace.GetComponent<Enemy_variables>().speed * -1;
        }
        current_smallPlanes += 1;
        current_spawnTimeSmallPlanes = spawnTimeSmallPlanes + Random.Range(-1f, 1f);

    }

    public void SpawnAllyPlane()
    {
        var randomSpawner = Random.Range(0, 2);
        if (randomSpawner == 0)
        {
            Instantiate(allyPlanePrefab, topLeftSpawner.transform.position, topLeftSpawner.transform.rotation);
        }
        else
        {
            GameObject enemy_instace = Instantiate(allyPlanePrefab, topRightSpawner.transform.position, topRightSpawner.transform.rotation);
            enemy_instace.GetComponent<Enemy_variables>().speed = enemy_instace.GetComponent<Enemy_variables>().speed * -1;
        }
        current_allyPlanes += 1;
        current_spawnTimeAllyPlanes = spawnTimeAllyPlanes + Random.Range(-3f, 3f);

    }
}
