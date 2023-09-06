using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //Enemy Type//
    public bool isZombie;
    public bool isDog;
    public bool isBlubbergeist;


    //Other variables//
    private Animator animator;
    public GameObject HeadCollider;
    public GameObject BodyCollider;
    public GameObject DogCollider;
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
    public AudioClip bodyshotSound;
    public AudioClip zombieDeadSound;
    public AudioClip dogDeadSound;

    //Attack//
    private bool hasAttacked;

    //Destroy Timer//
    public float DestroyTimer;
    private float current_destroyTimer;

    //Collider Layer//
    private LayerMask aliveLayer;
    private LayerMask deadLayer;

    //Subject//



    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>(); 
       audioSource = GetComponent<AudioSource>();
       current_speed = speed;

        if (HeadCollider != null)
            HeadCollider.layer = aliveLayer;
        if (BodyCollider != null)
        {
            BodyCollider.layer = aliveLayer;
        }
            
        if (DogCollider != null)
            DogCollider.layer = aliveLayer;

        aliveLayer = LayerMask.NameToLayer("Alive");
        deadLayer = LayerMask.NameToLayer("Dead");

        current_health = health;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();



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
        if (DogCollider != null)
            DogCollider.layer = aliveLayer;
        current_speed = speed;
    }

    public void EnemyDead(string anim, float blood)
    {
        if (isAlive)
        {
            animator.Play(anim, -1, 0f);
            if (HeadCollider != null)
                HeadCollider.layer = deadLayer;
            if (BodyCollider != null)
            {
                BodyCollider.layer = deadLayer;

            }

            PlayerController.current_bloodBarCapacity += blood;
            isAlive = false;
            current_speed = 0f;

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
            if (isZombie)
                audioSource.PlayOneShot(zombieDeadSound);
            if (isDog)
                audioSource.PlayOneShot(dogDeadSound);
        }
        else
            PlayBodyshotAnimation();
    }
    public void Movement()
    {
        transform.position = transform.position + new Vector3(-current_speed * Time.deltaTime, 0f, 0f);
    }

    //public void AttackPlayer()
    //{
    //    if (hasAttacked == false)
    //    {
    //        hasAttacked = true;
    //        //Destroy(gameObject);
    //        //Attack animation
    //    }

    //}

    public void PlayHeadshot()
    {
        audioSource.PlayOneShot(headshotSound);
    }

    public void PlayBodyshot()
    {
        audioSource.PlayOneShot(bodyshotSound, 0.3f);
    }

    public void PlayBodyshotAnimation()
    {
        animator.Play("zombie1_bodyshot", -1, 0f);
    }
    public void DisableGameobject()
    {
        gameObject.SetActive(false);
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
