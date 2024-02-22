using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTrigger : MonoBehaviour
{
    [SerializeField] private int enemiesEaten;
    [SerializeField] private int maxEnemiesEaten;
    [SerializeField] private string targetName;

    private Vector3 initialSize;
    [SerializeField] private Vector3 sizeIncrease;
    [SerializeField] private SoldierMovement soldierMovement;
    [SerializeField] private float changeSpeedValue;

    private void Start()
    {
        initialSize = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (enemiesEaten < maxEnemiesEaten)
        {
            if (collision.transform.tag == targetName)
            {
                IncreaseUnitsEaten();
                collision.GetComponent<EnemyMovement>().DeleteTarget();
                Destroy(collision.gameObject);
                transform.localScale += sizeIncrease;
                soldierMovement.ChangeSpeed(-changeSpeedValue);

                //IF FULL AFTER EATING, GO NEST//
                if (enemiesEaten == maxEnemiesEaten)
                {
                    soldierMovement.isFull = true;

                }
            }

        }

        //DROP IN NEST//
        if (enemiesEaten == maxEnemiesEaten)
        {
            if (collision.transform.tag == "Nest")
            {
                //PlayerWorldVariables.lumisInNest += workersEaten;
                Actions.OnLumiDrop();
                transform.localScale = initialSize;
                enemiesEaten = 0;
                soldierMovement.isFull = false;
                //Destroy(gameObject);
            }
        }
    }

    private void IncreaseUnitsEaten()
    {
        ++enemiesEaten;
    }
}
