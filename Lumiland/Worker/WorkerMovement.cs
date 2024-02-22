using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMovement : MonoBehaviour
{
    [SerializeField] private bool targetDetected;
    [SerializeField] private GameObject targetLumi;
    [SerializeField] private GameObject nest;
    [SerializeField] private WorkerTargetScanner targetScanner;
    [SerializeField] private float movSpeed;
    [SerializeField] public bool isTargeted;

    [SerializeField] public bool isFull;
    private void Update()
    {
        if (isFull == false)
        {
            if (targetDetected == true)
            {
                ChaseLumi();
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
        targetLumi = targetToFollow;
    }

    public void ChangeSpeed(float changeSpeedValue)
    {
        movSpeed += changeSpeedValue;
    }

    private void ChaseLumi()
    {
        if (targetLumi != null)
        {
            Vector2 workerPosition = transform.position;
            Vector2 targetLumiPosition = targetLumi.transform.position;
            Vector2 moveDir = (targetLumiPosition - workerPosition).normalized;

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
        Vector2 workerPosition = transform.position;
        Vector2 targetNestPosition = PlayerWorldVariables.nestPosition;
        Vector2 moveDir = (targetNestPosition - workerPosition).normalized;

        transform.position += new Vector3(moveDir.x, moveDir.y, 0f) * movSpeed * Time.deltaTime;
    }

    public void WorkerTargeted()
    {
        isTargeted = true;
        Invoke("WorkerUntargeted", 1f);
    }

    public void WorkerUntargeted()
    {
        isTargeted = false;
    }
}
