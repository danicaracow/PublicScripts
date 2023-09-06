using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{


    //GROUND MOVEMENT//
    public static float groundSpeed = -1f;
    public static float backgroundSpeed = -0.5f;

    //Enemies killed//
    public static int totalEnemiesKilled;
    private int enemiesKilledDifficulty;
    public int enemiesKilledDifficultyTrigger;

    //Pause Manager//
    public PauseManager pauseManager;

    //Enemy object pool//
    //public GameObject bulletSpawner;
    public GameObject bulletPrefab;
    public GameObject BulletsPool;
    public int bulletPoolSize;
    public static int staticBulletPoolSize;
    public static GameObject[] bullets;
    public static int shootNumber = -1;




    private void Start()
    {
        //Spit object pool//
        staticBulletPoolSize = bulletPoolSize;

        bullets = new GameObject[staticBulletPoolSize];

        for (int i = 0; i < staticBulletPoolSize; i++)
        {
            bullets[i] = Instantiate(bulletPrefab);
            bullets[i].transform.parent = BulletsPool.transform;
            bullets[i].SetActive(false);
        }

        //Enemies killed//
        totalEnemiesKilled = 0;
        enemiesKilledDifficulty = 0;
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        Actions.OnEnemyKilled += EnemyKill;
        totalEnemiesKilled = 0;
        pauseManager.DisplayDeadCountText();
    }

    private void OnDisable()
    {
        Actions.OnEnemyKilled -= EnemyKill;
    }

    public static void CheckArrayRange()
    {
        if (shootNumber >= staticBulletPoolSize)
        {
            shootNumber = 0;
        }
    }

    public void EnemyKill()
    {
        totalEnemiesKilled += 1;
        enemiesKilledDifficulty += 1;

        pauseManager.DisplayDeadCountText();
        pauseManager.DisplayDeadCountAnimation();

        if (enemiesKilledDifficulty >= enemiesKilledDifficultyTrigger)
        {
            Actions.OnIncreaseDificulty();
            enemiesKilledDifficulty = 0;
        }
    }


}
