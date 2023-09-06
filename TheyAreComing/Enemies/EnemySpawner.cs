using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Spawner type//
    public bool isEnemySpawner;
    public bool isObstacleSpawner;
    public bool isPropSpawner;
    public bool isBuildingSpawner;
    public bool isContainerSpawner;

    public bool isActivated;



    //Prefabs//
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> propList = new List<GameObject>();
    public List<GameObject> buildingList = new List<GameObject>();
    public List<GameObject> obstacleList = new List<GameObject>();
    public GameObject containerprefab;
    //public GameObject zombie1Prefab;
    //public GameObject dogPrefab;

    //Spawn variables//
    private Vector3 _spawnPosition;
    private float _randomYposition;
    public float _minimumYposition = 4f;
    public float _maximumYposition = -3.5f;
    public float _timer;
    public float MinimumRandomTimerRange;
    public float MaximumRandomTimerRange = 5;
    private float _current_timer;


    //OBJECT POOL//
    //Enemies//
    public GameObject Zombies1Pool;
    public GameObject DogsPool;
    public GameObject BlubbergeistsPool;
    public GameObject SpitterPool;
    private GameObject[] zombies1;
    private GameObject[] dogs;
    private GameObject[] blubbergeists;
    private GameObject[] spitters;
    private int zombies1Number = -1;
    private int dogsNumber = -1;
    private int blubbergeistNumber = -1;
    private int spitterNumber = -1;
    public int Zombies1PoolSize;
    public int DogsPoolSize;
    public int BlubbergeistPoolSize;
    public int SpitterPoolSize;

    //Obstacles//
    public GameObject obstaclePrefab;
    public GameObject ObstaclesPool;
    private GameObject[] obstacles;
    private int obstaclesNumber = -1;
    public int ObstaclesPoolSize;
    

    //Props//
    public GameObject DeadTrees1Pool;
    public GameObject DeadTrees2Pool;
    private GameObject[] deadTrees1;
    private GameObject[] deadTrees2;
    private int deadTrees1Number = -1;
    private int deadTrees2Number = -1;
    public int DeadTrees1PoolSize;
    public int DeadTrees2PoolSize;

    //Container//
    public GameObject ContainersPool;
    private GameObject[] containers;
    private int containersNumber = -1;
    public int ContainerPoolSize;

    //Sounds//
    private AudioSource audioSource;
    public AudioClip zombieSpawn;
    public AudioClip dogSpawn;
    public AudioClip blubbergeistSpawn;
    public AudioClip spitterSpawn;

    //Difficulty//
    public float difficultyIncrease;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        _randomYposition = Random.Range(_minimumYposition, _maximumYposition);
        _timer = Random.Range(MinimumRandomTimerRange, MaximumRandomTimerRange);
        _current_timer = _timer;

        if (isEnemySpawner)
        {
            zombies1 = new GameObject[Zombies1PoolSize];
            dogs = new GameObject[DogsPoolSize];
            blubbergeists = new GameObject[BlubbergeistPoolSize];
            spitters = new GameObject[SpitterPoolSize];

            for (int i = 0; i < Zombies1PoolSize; i++)
            {
                zombies1[i] = Instantiate(enemyList[0], transform.position, transform.rotation);
                zombies1[i].transform.parent = Zombies1Pool.transform;
                zombies1[i].SetActive(false);
            }

            for (int i = 0; i < DogsPoolSize; i++)
            {
                dogs[i] = Instantiate(enemyList[1], transform.position, transform.rotation);
                dogs[i].transform.parent = DogsPool.transform;
                dogs[i].SetActive(false);
            }

            for (int i = 0; i < BlubbergeistPoolSize; i++)
            {
                blubbergeists[i] = Instantiate(enemyList[2], transform.position, transform.rotation);
                blubbergeists[i].transform.parent = BlubbergeistsPool.transform;
                blubbergeists[i].SetActive(false);
            }

            for (int i = 0; i < SpitterPoolSize; i++)
            {
                spitters[i] = Instantiate(enemyList[3], transform.position, transform.rotation);
                spitters[i].transform.parent = SpitterPool.transform;
                spitters[i].SetActive(false);
            }
        }

        if (isPropSpawner)
        {
            deadTrees1 = new GameObject[DeadTrees1PoolSize];
            deadTrees2 = new GameObject[DeadTrees2PoolSize];

            for (int i = 0; i < DeadTrees1PoolSize; i++)
            {
                deadTrees1[i] = Instantiate(propList[0], transform.position, transform.rotation);
                deadTrees1[i].transform.parent = DeadTrees1Pool.transform;
                deadTrees1[i].SetActive(false);
            }

            for (int i = 0; i < DeadTrees2PoolSize; i++)
            {
                deadTrees2[i] = Instantiate(propList[1], transform.position, transform.rotation);
                deadTrees2[i].transform.parent = DeadTrees2Pool.transform;
                deadTrees2[i].SetActive(false);
            }
        }

        //if (isBuildingSpawner)
        //{
        //        _timer = Random.Range(MinimumRandomTimerRange, MaximumRandomTimerRange);
        //        _current_timer = _timer;
        //        SpawnBuilding();
        //}

        if (isObstacleSpawner)
        {
            obstacles = new GameObject[ObstaclesPoolSize];

            for (int i = 0; i < ObstaclesPoolSize; i++)
            {
                obstacles[i] = Instantiate(obstaclePrefab, transform.position, transform.rotation);
                obstacles[i].transform.parent = ObstaclesPool.transform;
                obstacles[i].SetActive(false);
            }
        }

        if (isContainerSpawner)
        {
            containers = new GameObject[ContainerPoolSize];

            for (int i = 0; i < ContainerPoolSize; i++)
            {
                containers[i] = Instantiate(containerprefab, transform.position, transform.rotation);
                containers[i].transform.parent = ContainersPool.transform;
                containers[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {


            Timer();


            if (isEnemySpawner)
            {

                if (_current_timer <= 0)
                {
                    _timer = Random.Range(MinimumRandomTimerRange, MaximumRandomTimerRange);
                    _current_timer = _timer;
                    SpawnEnemy();
                }

            }

            if (isPropSpawner)
            {
                if (_current_timer <= 0)
                {
                    _timer = Random.Range(MinimumRandomTimerRange, MaximumRandomTimerRange);
                    _current_timer = _timer;
                    SpawnPrompt();
                }
            }

            if (isBuildingSpawner)
            {
                if (_current_timer <= 0)
                {
                    _timer = Random.Range(MinimumRandomTimerRange, MaximumRandomTimerRange);
                    _current_timer = _timer;
                    SpawnBuilding();
                }
            }

            if (isObstacleSpawner)
            {
                if (_current_timer <= 0)
                {
                    _timer = Random.Range(MinimumRandomTimerRange, MaximumRandomTimerRange);
                    _current_timer = _timer;
                    SpawnObstacle();
                }
            }

            if (isContainerSpawner)
            {
                if (_current_timer <= 0)
                {
                    _timer = Random.Range(MinimumRandomTimerRange, MaximumRandomTimerRange);
                    _current_timer = _timer;
                    SpawnContainer();
                }
            }
        }

    }

    //ACTIONS//
    private void OnEnable()
    {
        Actions.OnIncreaseDificulty += IncreaseDifficulty;
    }


    private void SpawnEnemy()
    {
        _randomYposition = Random.Range(_minimumYposition, _maximumYposition);
        transform.position = new Vector3(transform.position.x, _randomYposition, transform.position.z);
        
        int randomEnemy = Random.Range(1, 100);
        if (randomEnemy >= 50)
        {
            zombies1Number++;
            // Check if we exceeded array's range
            if (zombies1Number >= Zombies1PoolSize)
            {
                zombies1Number = 0;
            }

            // Zombie placement and activation
            zombies1[zombies1Number].transform.position = transform.position;
            zombies1[zombies1Number].SetActive(true);

            //Spawn Sound
            audioSource.PlayOneShot(zombieSpawn);
        }
        else if (randomEnemy >= 20)
        {
            dogsNumber++;
            if (dogsNumber >= DogsPoolSize)
            {
                dogsNumber = 0;
            }

            // Dog placement and activation
            dogs[dogsNumber].transform.position = transform.position;
            dogs[dogsNumber].SetActive(true);

            //Spawn Sound
            audioSource.PlayOneShot(dogSpawn);
        }
        else if (randomEnemy >= 10)
        {
            blubbergeistNumber++;
            if (blubbergeistNumber >= BlubbergeistPoolSize)
            {
                blubbergeistNumber = 0;
            }

            // Blubbergeist placement and activation
            blubbergeists[blubbergeistNumber].transform.position = transform.position;
            blubbergeists[blubbergeistNumber].SetActive(true);

            //Spawn Sound
            audioSource.PlayOneShot(blubbergeistSpawn);
        }
        else
        {
            spitterNumber++;
            if (spitterNumber >= SpitterPoolSize)
            {
                spitterNumber = 0;
            }

            // Spitter placement and activation
            spitters[spitterNumber].transform.position = transform.position;
            spitters[spitterNumber].SetActive(true);

            //Spawn Sound
            audioSource.PlayOneShot(spitterSpawn);
        }

    }

    private void SpawnPrompt()
    {
        _randomYposition = Random.Range(_minimumYposition, _maximumYposition);
        transform.position = new Vector3(transform.position.x, _randomYposition, transform.position.z);

        int randomIndex = Random.Range(0, propList.Count);
        if (randomIndex == 0)
        {
            deadTrees1Number++;
            // Check if we exceeded array's range
            if (deadTrees1Number >= DeadTrees1PoolSize)
            {
                deadTrees1Number = 0;
            }

            deadTrees1[deadTrees1Number].transform.position = transform.position;
            deadTrees1[deadTrees1Number].SetActive(true);
        }
        else
        {
            deadTrees2Number++;
            // Check if we exceeded array's range
            if (deadTrees2Number >= DeadTrees2PoolSize)
            {
                deadTrees2Number = 0;
            }

            deadTrees2[deadTrees2Number].transform.position = transform.position;
            deadTrees2[deadTrees2Number].SetActive(true);
        }
        //Instantiate(prefabToSpawn, transform.position, transform.rotation);
    }

    private void SpawnBuilding()
    {
        //_randomYposition = Random.Range(_minimumYposition, _maximumYposition);
        //transform.position = new Vector3(transform.position.x, _randomYposition, transform.position.z);

        int randomIndex = Random.Range(0, buildingList.Count);
        GameObject prefabToSpawn = buildingList[randomIndex];

        Instantiate(prefabToSpawn, transform.position, transform.rotation);


    }

    private void SpawnObstacle()
    {
        obstaclesNumber++;
        // Check if we exceeded array's range
        if (obstaclesNumber >= ObstaclesPoolSize)
        {
            obstaclesNumber = 0;
        }

        _randomYposition = Random.Range(_minimumYposition, _maximumYposition);
        transform.position = new Vector3(transform.position.x, _randomYposition, transform.position.z);

        //int randomIndex = Random.Range(0, buildingList.Count);
        //GameObject prefabToSpawn = obstacleList[randomIndex];

        obstacles[obstaclesNumber].transform.position = transform.position;
        obstacles[obstaclesNumber].SetActive(true);


    }

    private void SpawnContainer()
    {
        _randomYposition = Random.Range(_minimumYposition, _maximumYposition);
        transform.position = new Vector3(transform.position.x, _randomYposition, transform.position.z);

        containersNumber++;
        // Check if we exceeded array's range
        if (containersNumber >= ContainerPoolSize)
        {
            containersNumber = 0;
        }

        // Zombie placement and activation
        containers[containersNumber].transform.position = transform.position;
        containers[containersNumber].SetActive(true);
    }


    private void Timer()
    {
        if (_current_timer > 0)
        {
            _current_timer -= Time.deltaTime;
        }
    }

    public void IncreaseDifficulty()
    {
        if (isEnemySpawner)
        {
            if (MaximumRandomTimerRange > 1.25f)
            MaximumRandomTimerRange -= difficultyIncrease;
        }
        
    }
}
