using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "parachute")
        {
            collision.GetComponent<ParachuteCollider>().DestroyParachute();
        }

        if (collision.transform.tag == "soldier_enemy" ||
            collision.transform.tag == "soldier_ally")
        {
            collision.GetComponent<SoldierCollider>().KillSoldier();
        }
    }
}
