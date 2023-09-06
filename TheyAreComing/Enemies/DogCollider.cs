using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCollider : MonoBehaviour
{
    public EnemyController parentObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void DogHit()
    //{
    //    parentObject.EnemyDead("dog_dead_anim");
    //}

    //public void HitPlayer()
    //{
    //    parentObject.AttackPlayer();
    //}


    //DISABLE PARENT OBJECT WHEN HITTING LEFT MAP LIMIT//
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "map_limit")
        {
            parentObject.DisableGameobject();
        }
    }
}

