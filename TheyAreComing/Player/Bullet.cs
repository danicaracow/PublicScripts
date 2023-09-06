using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool isHit;
    public bool isEnemy;
    public float damage;

    public Collider2D firstCollider;
    // Start is called before the first frame update
    void Start()
    {
        damage = damage / 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        BulletMovement();
    }


    private void BulletMovement()
    {
        transform.position = transform.position + new Vector3(speed * Time.fixedDeltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "map_limit")
        {
            gameObject.SetActive(false);
        }


        //PLAYER BULLET//
        //if (collision.transform.tag == "bullet_limit")
        //{
        //    gameObject.SetActive(false);
        //}

        if (firstCollider != null)
            return;

        if (collision.transform.tag == "enemy")
        {
            HeadCollider headCollider = collision.GetComponent<HeadCollider>();
            BodyCollider bodyCollider = collision.GetComponent<BodyCollider>();
            //DogCollider dogCollider = collision.GetComponent<DogCollider>();
            if (headCollider != null)
            {
                headCollider.HeadHit();
            }
            if (bodyCollider != null)
            {
                bodyCollider.BodyHit();
            }
            //if (dogCollider != null)
            //{
            //    dogCollider.DogHit();
            //}

            gameObject.SetActive(false);

            firstCollider = collision;
        }

        if (collision.transform.tag == "container")
        {
            collision.GetComponent<Container>().GetHit();
            gameObject.SetActive(false);
        }


    }

    private void OnEnable()
    {
        firstCollider = null;
    }
}
