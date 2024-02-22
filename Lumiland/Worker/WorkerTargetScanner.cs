using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTargetScanner : MonoBehaviour
{
    [SerializeField] private WorkerMovement workerMovement;

    [SerializeField] private float movSpeed;
    [SerializeField] private float maxSize;
    [SerializeField] private Vector3 initialScale;

    [SerializeField] public GameObject target;
    [SerializeField] private bool isActivated = true;
    private void Start()
    {
        initialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if(isActivated)
        transform.localScale += new Vector3(movSpeed, movSpeed, 0f) * Time.deltaTime;

        if (transform.localScale.x >= maxSize)
        {
            transform.localScale = initialScale;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Lumi")
        {
            if (collision.gameObject.GetComponent<LumiMovement>().isTargeted == false)
            {
                workerMovement.TargetDetected(collision.gameObject);
                collision.gameObject.GetComponent<LumiMovement>().LumiTargeted();
                transform.localScale = initialScale;
                TurnOFFScanner();
            }
            
        }
    }

    public void TurnONScanner()
    {
        if (!isActivated)
            isActivated = true;
    }

    public void TurnOFFScanner()
    {
        if (isActivated)
            isActivated = false;
    }
}
