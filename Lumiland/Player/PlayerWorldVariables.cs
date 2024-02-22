using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldVariables : MonoBehaviour
{
    //LUMIS//
    [SerializeField] public static int lumisEaten;
    [SerializeField] public static int _maxLumisEaten;
    [SerializeField] private int maxLumisEaten;
    [SerializeField] public static int _lumisInNest;
    [SerializeField] private int lumisInNest;
    [SerializeField] public static int currentWorkersCount;
    [SerializeField] private static int _workerCost;
    [SerializeField] private int workerCost;
    [SerializeField] public static int currentSoldiersCount;
    [SerializeField] private static int _soldierCost;
    [SerializeField] private int soldierCost;

    [SerializeField] public static float _changeSpeedValue;
    [SerializeField] private float changeSpeedValue;

    //NEST//
    [SerializeField] public static int _nestHealth;
    [SerializeField] private int nestHealth;
    [SerializeField] private GameObject nest;
    [SerializeField] private GameObject enemyNest;
    public static Vector2 nestPosition;
    public static Vector2 enemyNestPosition;

    //SHOT//
    public static float _shotSpeed;
    public float shotSpeed;


    private void Awake()
    {
        nestPosition = nest.transform.position;
        enemyNestPosition = enemyNest.transform.position;
        _workerCost = workerCost;
        _soldierCost = soldierCost;
        _lumisInNest = lumisInNest;
        _shotSpeed = shotSpeed;
        _changeSpeedValue = changeSpeedValue;
        _maxLumisEaten = maxLumisEaten;
        _nestHealth = nestHealth;
    }

    public void BuyWorker()
    {
        if (_lumisInNest >= _workerCost)
        {
            _lumisInNest -= _workerCost;
            ++currentWorkersCount;
            Actions.OnWorkerSpawn();
        }
    }

    public void BuySoldier()
    {
        if (_lumisInNest >= _soldierCost)
        {
            _lumisInNest -= _soldierCost;
            ++currentSoldiersCount;
            Actions.OnSoldierSpawn();
        }
    }


}
