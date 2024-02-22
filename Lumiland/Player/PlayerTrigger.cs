using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
                     private Vector2 initialSize;
                     public Vector3 sizeIncrease;
    //[SerializeField] private float changeSpeedValue;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float dropTimer;
    [SerializeField] private float current_dropTimer;
    [SerializeField] private bool isNested;
    [SerializeField] private int lumisEaten;
    [SerializeField] private int maxLumisEaten;

    private void Start()
    {
        initialSize = transform.localScale;
    }

    private void Update()
    {
        if (isNested)
        {
            if (Input.GetMouseButton(0))
            {
                if (PlayerWorldVariables.lumisEaten > 0)
                {
                    current_dropTimer -= Time.deltaTime;
                }


            }
            else
            {
                current_dropTimer = dropTimer;
            }
            if (current_dropTimer <= 0)
            {
                DropLumi();
            }
        }
        else
        {
            current_dropTimer = dropTimer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerWorldVariables.lumisEaten < PlayerWorldVariables._maxLumisEaten)
        {
            if (collision.transform.tag == "Lumi")
            {
                IncreaseLumisEaten();
                Destroy(collision.gameObject);
                transform.localScale += sizeIncrease;
                playerMovement.ChangeSpeed(-PlayerWorldVariables._changeSpeedValue);
            }
            
        }

        if (collision.transform.tag == "Nest" && isNested == false)
        {
            isNested = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Nest" && isNested == true)
        {
            isNested = false;
        }
    }

    private void DropLumi()
    {
        DecreaseLumisEaten();
        PlayerWorldVariables._lumisInNest += 1;
        Actions.OnLumiDrop();
        playerMovement.ChangeSpeed(PlayerWorldVariables._changeSpeedValue);
        transform.localScale -= sizeIncrease;
        current_dropTimer = dropTimer;
    }

    public void DecreaseLumisEaten()
    {
        PlayerWorldVariables.lumisEaten -= 1;
    }
    public void IncreaseLumisEaten()
    {
        PlayerWorldVariables.lumisEaten += 1;
    }
}
