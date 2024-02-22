using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    [SerializeField] private bool targetDetected;
    [SerializeField] private GameObject targetEnemy;
    //[SerializeField] private GameObject nest;
    [SerializeField] private SoldierTargetScanner targetScanner;
    [SerializeField] private float movSpeed;

    [SerializeField] public bool isFull;
    private void Update()
    {
        if (isFull == false)
        {
            if (targetDetected == true)
            {
                ChaseTarget();
            }
        }
        else
        {
            GoNest();
            targetScanner.TurnOFFScanner();
        }
    }

    public void TargetDetected(GameObject targetToFollow)
    {
        targetDetected = true;
        targetEnemy = targetToFollow;
    }

    public void ChangeSpeed(float changeSpeedValue)
    {
        movSpeed += changeSpeedValue;
    }

    private void ChaseTarget()
    {
        if (targetEnemy != null)
        {
            Vector2 soldierPosition = transform.position;
            Vector2 targetEnemyPosition = targetEnemy.transform.position;
            Vector2 moveDir = (targetEnemyPosition - soldierPosition).normalized;

            transform.position += new Vector3(moveDir.x, moveDir.y, 0f) * movSpeed * Time.deltaTime;
        }
        else
        {
            targetDetected = false;
            targetScanner.TurnONScanner();
        }
    }
    public void GoNest()
    {
        Vector2 soldierPosition = transform.position;
        Vector2 targetNestPosition = PlayerWorldVariables.nestPosition;
        Vector2 moveDir = (targetNestPosition - soldierPosition).normalized;

        transform.position += new Vector3(moveDir.x, moveDir.y, 0f) * movSpeed * Time.deltaTime;
    }
}
