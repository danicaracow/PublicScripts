using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void FixedUpdate()
    {
        transform.position += transform.up * PlayerWorldVariables._shotSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Map_Limit")
        {
            Destroy(gameObject);
        }

        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<EnemyMovement>().EnemyDead();
            Destroy(gameObject);
        }

        

    }
}
