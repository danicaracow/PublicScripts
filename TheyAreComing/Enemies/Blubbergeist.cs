using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blubbergeist : MonoBehaviour
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
    public AudioClip explosionSound;
    public AudioClip inflateSound;

    //Attack//
    private bool hasAttacked;
    public GameObject blubbergeistDetection;
    public GameObject blubbergeistExplosion;
    public GameObject blubbergeistSmoke;
    public float explosionDuration;


    //Collider Layer//
    private LayerMask aliveLayer;
    private LayerMask deadLayer;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        current_speed = speed;

        if (HeadCollider != null)
            HeadCollider.layer = aliveLayer;
        if (BodyCollider != null)
            BodyCollider.layer = aliveLayer;

        aliveLayer = LayerMask.NameToLayer("Alive");
        deadLayer = LayerMask.NameToLayer("Dead");

        current_health = health;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    private void OnEnable()
    {
        isAlive = true;

        current_health = health;

        if (HeadCollider != null)
            HeadCollider.layer = aliveLayer;
        if (BodyCollider != null)
            BodyCollider.layer = aliveLayer;

        current_speed = speed;

        blubbergeistDetection.SetActive(true);
        blubbergeistExplosion.SetActive(false);
        blubbergeistSmoke.SetActive(false);
    }

    public void EnemyDead(float blood)
    {
        if (isAlive)
        {
            //animator.Play(anim, -1, 0f);
            if (HeadCollider != null)
                HeadCollider.layer = deadLayer;
            if (BodyCollider != null)
                BodyCollider.layer = deadLayer;

            PlayerController.current_bloodBarCapacity += blood;
            isAlive = false;
            current_speed = 0f;

            if (audioSourceAmbient != null)
                audioSourceAmbient.Stop();

            Actions.OnEnemyKilled();
        }
    }

    public void DamageTaken(int damage, float blood)
    {
        current_health -= damage;
        if (current_health <= 0)
        {
            EnemyDead(blood);
            PlayBlubbergeistDeadAnimation();
            audioSource.PlayOneShot(deadSound);
            blubbergeistDetection.SetActive(false);
        }
        else
        {
            PlayBlubbergeistHeadshotAnimation();
        }
    }

    public void Explode()
    {
        EnemyDead(0f);
        PlayBlubbergeistBlowingUpAnimation();
        audioSource.PlayOneShot(inflateSound);
        blubbergeistDetection.SetActive(false);
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
        audioSource.PlayOneShot(headshotSound, 0.3f);
    }

    public void DisableGameobject()
    {
        gameObject.SetActive(false);
    }

    public void ExplosionDamageAndSmoke()
    {
        blubbergeistExplosion.SetActive(true);
        audioSource.PlayOneShot(explosionSound);
        Invoke("DisableExplosion", explosionDuration);
        
        blubbergeistSmoke.SetActive(true);
    }

    public void DisableExplosion()
    {
        blubbergeistExplosion.SetActive(false);
    }

    //ANIMATIONS//
    public void PlayBlubbergeistHeadshotAnimation()
    {
        animator.Play("blubbergeist_headshot", -1, 0f);
    }

    public void PlayBlubbergeistBlowingUpAnimation()
    {
        animator.Play("blubbergeist_blowingup", -1, 0f);
    }

    public void PlayBlubbergeistDeadAnimation()
    {
        animator.Play("blubbergeist_dead", -1, 0f);
    }

    
}
