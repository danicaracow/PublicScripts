using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCollider : MonoBehaviour
{
    
    public GameObject parentObject;
    private Parachute parachute;
    private Parachute_white parachuteWhite;

    private void Start()
    {
        parachute = parentObject.GetComponent<Parachute>();
        parachuteWhite = parentObject.GetComponent<Parachute_white>();

    }

    //Collides with bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (parachute != null)
        {
            parachute.EnemyDies();
        }

        if (parachuteWhite != null)
        {
            parachuteWhite.EnemyDies();
        }
    }


    //Collides with ground
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            if (parachute != null)
            {
                if (parachute.isFalling == true)
                {
                    parachute.EnemyDies();
                }
                else
                {
                    parachute.Landing();
                }
            }
            
            if (parachuteWhite != null)
            {
                if (parachuteWhite.isFalling == true)
                {
                    parachuteWhite.EnemyDies();
                }
                else
                {
                    parachuteWhite.Landing();
                }
            }
            

        }
        
    }

    public void KillSoldier()
    {
        if (parachute != null)
        {
            parachute.EnemyDies();
        }

        if (parachuteWhite != null)
        {
            parachuteWhite.EnemyDies();
        }
    }
}
