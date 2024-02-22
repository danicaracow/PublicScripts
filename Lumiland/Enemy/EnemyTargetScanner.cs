using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetScanner : MonoBehaviour
{
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyTrigger enemyTrigger;
    [SerializeField] private string targetName;

    [SerializeField] private float movSpeed;
    [SerializeField] private float maxSize;
    [SerializeField] private Vector3 initialScale;

    [SerializeField] public GameObject target;
    [SerializeField] private bool isActivated = true;
    [SerializeField] private bool isAttackingNest;
    private void Start()
    {
        initialScale = transform.localScale;
        TurnONScanner();
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
        if (collision.transform.tag == "Nest")
        {
            enemyMovement.TargetDetected(collision.gameObject);
            enemyTrigger.AttackingNest();
            transform.localScale = initialScale;
            TurnOFFScanner();
            return;
        }

        if (collision.transform.tag == targetName)
        {
            if (collision.gameObject.GetComponent<WorkerMovement>().isTargeted == false)
            {
                enemyMovement.TargetDetected(collision.gameObject);
                collision.gameObject.GetComponent<WorkerMovement>().WorkerTargeted();
                transform.localScale = initialScale;
                TurnOFFScanner();
            }

        }
        
        
        
    }

    public void TurnONScanner()
    {
        if (!isActivated)
        {
            gameObject.SetActive(true);
            isActivated = true;
        }
            
    }

    public void TurnOFFScanner()
    {
        if (isActivated)
        {
            gameObject.SetActive(false);
            isActivated = false;
        }
            
    }

    
}
