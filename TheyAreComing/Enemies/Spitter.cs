using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{

    //Other variables//
    private Animator animator;
    public GameObject HeadCollider;
    public GameObject BodyCollider;
    public float speed;
    public float current_speed;
    private bool isAlive = true;
    public bool isHit;



    //Health//
    public int health;
    public int current_health;

    //Sound//
    public AudioSource audioSource;
    public AudioSource audioSourceAmbient;
    public AudioClip headshotSound;
    public AudioClip deadSound;
    public AudioClip spitSound;
    public AudioClip metallicSound;

    //Attack//
    private bool hasAttacked;
    public float attackTimer;
    public float current_attackTimer;
    public GameObject bulletSpawner;
    //private GameObject bulletPrefab;

    //Object Pool//
    //public GameObject BulletsPool;
    //public int bulletPoolSize;
    //private GameObject[] bullets;
    //private int shootNumber = -1;

    //Destroy Timer//
    public float DestroyTimer;
    private float current_destroyTimer;

    //Collider Layer//
    private LayerMask aliveLayer;
    private LayerMask deadLayer;




    // Start is called before the first frame update
    void Start()
    {
        //Animator//
        animator = GetComponent<Animator>();
        
        //Sound//
        audioSource = GetComponent<AudioSource>();
        
        //Enemy variables//
        current_speed = speed;
        current_health = health;

        //Collision//
        if (HeadCollider != null)
            HeadCollider.layer = aliveLayer;
        if (BodyCollider != null)
            BodyCollider.layer = aliveLayer;

        aliveLayer = LayerMask.NameToLayer("Alive");
        deadLayer = LayerMask.NameToLayer("Dead");

        //Attack//
        current_attackTimer = attackTimer;

        //Object Pool//
        //bulletPrefab = GameObject.FindGameObjectWithTag("enemy_bullets_pool");

        //bullets = new GameObject[bulletPoolSize];

        //for (int i = 0; i < bulletPoolSize; i++)
        //{
        //    bullets[i] = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        //    bullets[i].transform.parent = BulletsPool.transform;
        //    bullets[i].SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        

        if (isAlive)
        {
            Reload();

            if (current_attackTimer <= 0)
            {
                animator.Play("spitter_attack", -1, 0f);
                current_attackTimer = attackTimer;
            }
        }
        


    }

    //private void OnEnable()
    //{
    //    Debug.Log("REINICIADO");
    //    isAlive = true;
    //    animator = GetComponent<Animator>();
    //    animator.Play("zombie1_walking", -1, 0f);   //SI SE ACTIVA DA ERROR!!!

    //    if (HeadCollider != null)
    //        HeadCollider.layer = aliveLayer;
    //    if (BodyCollider != null)
    //        BodyCollider.layer = aliveLayer;
    //    if (DogCollider != null)
    //        DogCollider.layer = aliveLayer;
    //    current_speed = speed;
    //}

    private void OnEnable()
    {
        isAlive = true;
        //animator = GetComponent<Animator>();
        //animator.Play("zombie1_walking", -1, 0f);   //SI SE ACTIVA DA ERROR!!!

        if (HeadCollider != null)
            HeadCollider.layer = aliveLayer;
        if (BodyCollider != null)
            BodyCollider.layer = aliveLayer;
        current_speed = speed;
    }

    public void EnemyDead(string anim, float blood)
    {
        if (isAlive)
        {
            animator.Play(anim, -1, 0f);
            PlayDeadSound();

            //PlayHeadshot();
            if (HeadCollider != null)
                HeadCollider.layer = deadLayer;
            if (BodyCollider != null)
                BodyCollider.layer = deadLayer;
            isAlive = false;
            current_speed = 0f;

            PlayerController.current_bloodBarCapacity += blood;
            if (audioSourceAmbient != null)
                audioSourceAmbient.Stop();

            Actions.OnEnemyKilled();
        }
    }

    public void DamageTaken(string anim, int damage, float blood)
    {
        current_health -= damage;
        if (current_health <= 0)
        {
            EnemyDead(anim, blood);
        }
    }
    public void Movement()
    {
        transform.position = transform.position + new Vector3(-current_speed * Time.deltaTime, 0f, 0f);
    }

    public void Attack()
    {
        audioSource.PlayOneShot(spitSound);

        WorldManager.shootNumber++;

        // Check if we exceeded array's range
        //if (WorldManager.shootNumber >= WorldManager.bulletPoolSize)
        //{
        //    WorldManager.shootNumber = 0;
        //}
        WorldManager.CheckArrayRange();
        // Bullet placement and activation
        WorldManager.bullets[WorldManager.shootNumber].transform.position = bulletSpawner.transform.position;
        WorldManager.bullets[WorldManager.shootNumber].SetActive(true);
        

        

    }

    public void Reload()
    {
        if (current_attackTimer > 0)
        current_attackTimer -= Time.deltaTime;
    }

    public void PlayHeadshot()
    {
        audioSource.PlayOneShot(headshotSound);
    }

    public void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadSound);
    }

    public void PlayMetallicSound()
    {
        audioSource.PlayOneShot(metallicSound);
    }

    public void DisableGameobject()
    {
        gameObject.SetActive(false);
    }

    public void EnableHeadCollider()
    {
        HeadCollider.SetActive(true);
    }

    public void DisableHeadCollider()
    {
        HeadCollider.SetActive(false);
    }


    //ANIMATIONS//

    public void PlayZombieBodyDeadAnimation()
    {
        animator.Play("zombie1_body_dead", -1, 0f);
    }

    public void PlayZombieHeadDeadAnimation()
    {
        animator.Play("zombie1_dead", -1, 0f);
    }

    public void PlayDogDeadAnimation()
    {
        animator.Play("dog_dead_anim", -1, 0f);
    }
}
