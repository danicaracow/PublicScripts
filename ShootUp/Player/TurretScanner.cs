using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScanner : MonoBehaviour
{
    private Vector2 initialPosition;
    private float distance = 5f;
    public float scanSpeed;
    public bool isActivated;
    //public Turret_controller turretController;
    public List<GameObject> turretControllerList = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        turretControllerList.AddRange(GameObject.FindGameObjectsWithTag("turret"));
    }
    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            if (turretControllerList[0].GetComponent<Turret_controller>().TargetDetected == false)
            {
                ScanTarget();
            }
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated)
        {
            if (collision.transform.tag == "soldier_enemy")
            {
                GameObject target = collision.GetComponent<SoldierCollider>().parentObject.gameObject;
                if (target.GetComponent<Parachute>().isFalling == false)
                {
                    if (turretControllerList != null)
                    {
                        foreach (GameObject turret in turretControllerList)
                        {
                            turret.GetComponent<Turret_controller>().TargetDetected = true;
                            turret.GetComponent<Turret_controller>().target = target;
                        }
                    }


                    transform.position = initialPosition;
                }

            }
        }
        
    }
    private void ScanTarget()
    {
        if (transform.position.y < distance)
        {
            transform.position = new Vector2(initialPosition.x, transform.position.y + scanSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = initialPosition;
        }
    }
}
