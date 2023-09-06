using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour
{
    public GameObject parentObject;

    private EnemyController enemyController;
    private Blubbergeist blubbergeist;
    private Spitter spitter;

    //BLOOD//
    public float zombieBlood;
    public float dogBlood;

    // Start is called before the first frame update
    void Start()
    {
        if (parentObject.GetComponent<EnemyController>() != null)
        {
            enemyController = parentObject.GetComponent<EnemyController>();
        }

        if (parentObject.GetComponent<Blubbergeist>() != null)
        {
            blubbergeist = parentObject.GetComponent<Blubbergeist>();
        }

        if (parentObject.GetComponent<Spitter>() != null)
        {
            spitter = parentObject.GetComponent<Spitter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BodyHit()
    {
        if (enemyController != null)
        {
            if (enemyController.isZombie)
            {
                enemyController.DamageTaken("zombie1_body_dead", 1, zombieBlood);
                enemyController.PlayBodyshot();
            }
                

            if (enemyController.isDog)
            {
                enemyController.DamageTaken("dog_dead_anim", 1, dogBlood);
                enemyController.PlayBodyshot();
            }
                
        }
        

        if (blubbergeist != null)
        {
            blubbergeist.Explode();
        }

        if (spitter != null)
        {
            spitter.PlayMetallicSound();
        }

    }

    //public void HitPlayer()
    //{
    //    if (enemyController != null)
    //        enemyController.AttackPlayer();

    //    if (blubbergeist != null)
    //        blubbergeist.AttackPlayer();
    //}

    //DISABLE PARENT OBJECT WHEN HITTING LEFT MAP LIMIT//
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "map_limit")
        {
            parentObject.SetActive(false);
        }
    }
}
