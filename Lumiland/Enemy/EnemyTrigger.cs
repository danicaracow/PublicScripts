using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private int workersEaten;
    [SerializeField] private int maxWorkersEaten;
    [SerializeField] private string targetName;

    private Vector3 initialSize;
    [SerializeField] private Vector3 sizeIncrease;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private float changeSpeedValue;

    private bool isAttackingNest;

    private void Start()
    {
        initialSize = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttackingNest)
        {
            if (workersEaten < maxWorkersEaten)
            {
                if (collision.transform.tag == targetName)
                {
                    IncreaseWorkersEaten();
                    --PlayerWorldVariables.currentWorkersCount;
                    Actions.OnWorkerEaten();
                    Destroy(collision.gameObject);
                    transform.localScale += sizeIncrease;
                    enemyMovement.ChangeSpeed(-changeSpeedValue);

                    //IF FULL AFTER EATING, GO NEST//
                    if (workersEaten == maxWorkersEaten)
                    {
                        enemyMovement.isFull = true;
                        enemyMovement.targetScanner.TurnOFFScanner();
                    }
                }

            }
        }
        else
        {
            if (collision.transform.tag == "Nest")
            {
                PlayerWorldVariables._nestHealth--;
                Actions.OnNestDamaged();
                Destroy(gameObject);

            }
        }
        

        //DROP IN NEST//
        if (workersEaten == maxWorkersEaten)
        {
            if (collision.transform.tag == "Map_Limit")
            {
                //PlayerWorldVariables.lumisInNest += workersEaten;
                //Actions.OnLumiDrop();
                //transform.localScale = initialSize;
                //workersEaten = 0;
                //enemyMovement.isFull = false;
                Destroy(gameObject);
            }
        }
    }

    private void IncreaseWorkersEaten()
    {
        ++workersEaten;
    }

    public void AttackingNest()
    {
        isAttackingNest = true;
    }
}
