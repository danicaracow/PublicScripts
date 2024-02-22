using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTargetScanner : MonoBehaviour
{
    [SerializeField] private SoldierMovement soldierMovement;
    [SerializeField] private string targetName;

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
        if (isActivated)
            transform.localScale += new Vector3(movSpeed, movSpeed, 0f) * Time.deltaTime;

        if (transform.localScale.x >= maxSize)
        {
            transform.localScale = initialScale;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == targetName)
        {
            if (collision.gameObject.GetComponent<EnemyMovement>().isTargeted == false)
            {
                soldierMovement.TargetDetected(collision.gameObject);
                collision.gameObject.GetComponent<EnemyMovement>().EnemyTargeted();
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
