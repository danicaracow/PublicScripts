using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private bool targetDetected;
    [SerializeField] private GameObject targetWorker;
    //[SerializeField] private GameObject nest;
    [SerializeField] public EnemyTargetScanner targetScanner;
    [SerializeField] private float movSpeed;
    [SerializeField] public bool isTargeted;

    //DEAD//
    [SerializeField] private GameObject bloodParticlesPrefab;
    [SerializeField] private GameObject parentObject;

    [SerializeField] public bool isFull;

    private void Update()
    {
        if (isFull == false)
        {
            if (targetDetected == true)
            {
                ChaseWorker();
            }
        }
        else
        {
            GoNest();
            DeleteTarget();
            targetScanner.TurnOFFScanner();
        }
    }

    public void TargetDetected(GameObject targetToFollow)
    {
        targetDetected = true;
        targetWorker = targetToFollow;
    }

    public void ChangeSpeed(float changeSpeedValue)
    {
        movSpeed += changeSpeedValue;
    }

    private void ChaseWorker()
    {
        if (targetWorker != null)
        {
            Vector2 enemyPosition = transform.position;
            Vector2 targetWorkerPosition = targetWorker.transform.position;
            Vector2 moveDir = (targetWorkerPosition - enemyPosition).normalized;

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
        Vector2 enemyPosition = transform.position;
        Vector2 targetNestPosition = PlayerWorldVariables.enemyNestPosition;
        Vector2 moveDir = (targetNestPosition - enemyPosition).normalized;

        transform.position += new Vector3(moveDir.x, moveDir.y, 0f) * movSpeed * Time.deltaTime;
    }

    public void EnemyTargeted()
    {
        isTargeted = true;
    }

    public void DeleteTarget()
    {
        if (targetWorker != null)
        {
            targetWorker.GetComponent<WorkerMovement>().WorkerUntargeted();
        }
    }

    public void EnemyDead()
    {

        Instantiate(bloodParticlesPrefab, transform.position, transform.rotation);
        Destroy(parentObject);
    }
}
