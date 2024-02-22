using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerTrigger playerTrigger;
    [SerializeField] private GameObject shotPrefab;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerTrigger = GetComponent<PlayerTrigger>();
    }

    private void Update()
    {
        if (playerMovement.isShotMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (PlayerWorldVariables.lumisEaten > 0)
                {
                    Shot();
                    playerMovement.ChangeSpeed(PlayerWorldVariables._changeSpeedValue);
                    transform.localScale -= playerTrigger.sizeIncrease;
                }
                
            }
        }

    }


    private void Shot()
    {
        Instantiate(shotPrefab, transform.position, transform.rotation);
        PlayerWorldVariables.lumisEaten--;
    }


    //void ShotRayCast()
    //{
    //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition, defaultDistance);

    //    if (hit.collider != null)
    //    {
    //        Debug.Log("hit");
    //        ShotVisuals(transform.position, hit.point);
    //    }
    //    else
    //    {
    //        Debug.Log("no hit");
    //        ShotVisuals(transform.position, mousePosition);
    //    }
    //}

    //void ShotVisuals(Vector2 startPoint, Vector2 endPoint)
    //{
    //    lineRenderer.SetPosition(0, startPoint);
    //    lineRenderer.SetPosition(1, endPoint);
    //}
}
