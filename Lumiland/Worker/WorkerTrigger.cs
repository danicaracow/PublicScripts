using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTrigger : MonoBehaviour
{
    [SerializeField] private int lumisEaten;
    [SerializeField] private int maxLumisEaten;

                     private Vector3 initialSize;
    [SerializeField] private Vector3 sizeIncrease;
    [SerializeField] private WorkerMovement workerMovement;
    [SerializeField] private float changeSpeedValue;

    private void Start()
    {
        initialSize = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (lumisEaten < maxLumisEaten)
        {
            if (collision.transform.tag == "Lumi")
            {
                IncreaseLumisEaten();
                Destroy(collision.gameObject);
                transform.localScale += sizeIncrease;
                workerMovement.ChangeSpeed(-changeSpeedValue);

                //IF FULL AFTER EATING, GO NEST//
                if (lumisEaten == maxLumisEaten)
                {
                    workerMovement.isFull = true;

                }
            }

        }

        //DROP IN NEST//
        if (lumisEaten == maxLumisEaten)
        {
            if (collision.transform.tag == "Nest")
            {
                PlayerWorldVariables._lumisInNest += lumisEaten;
                Actions.OnLumiDrop();
                transform.localScale = initialSize;
                lumisEaten = 0;
                workerMovement.isFull = false;

            }
        }
    }

    private void IncreaseLumisEaten()
    {
        ++lumisEaten;
    }


}
