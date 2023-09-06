using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_movement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public GameObject playerReference;
    public float rotationSpeed;

    private void Start()
    {
        playerReference = referenceManager.playerReference;
        rb = GetComponent<Rigidbody>();
    }
    public void Rotacion()
    {
        Vector3 playerPos = (playerReference.transform.position - transform.position).normalized;
        Vector3 lookPos = playerPos;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
    }

    public void Movement_spawn()
    {
        rb.AddRelativeForce(0, 0, 16);
    }


    public void Movement()
    {
        rb.AddRelativeForce(0, 0, speed);
    }

}
        
