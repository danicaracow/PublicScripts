using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    public GameObject parentObject;
    
    private EnemyController enemyController;
    private Blubbergeist blubbergeist;
    private Spitter spitter;

    public AudioManager audioManager;

    //BLOOD//
    public float zombieBlood;
    public float blubbergeistBlood;
    public float spitterBlood;

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

    public void HeadHit()
    {
        if (enemyController)
        {
            if (enemyController.isZombie)
            {
                enemyController.DamageTaken("zombie1_dead", 2, zombieBlood);
                enemyController.PlayHeadshot();
            }
        }
        
        if (blubbergeist != null)
        {
            blubbergeist.DamageTaken(1, blubbergeistBlood);
            blubbergeist.PlayHeadshot();
        }

        if (spitter != null)
        {
            spitter.DamageTaken("spitter_dead", 3, spitterBlood);
            spitter.PlayHeadshot();
        }
    }

    //public void HitPlayer()
    //{
    //    enemyController.AttackPlayer();
    //}
}
