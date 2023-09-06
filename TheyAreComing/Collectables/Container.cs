using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    //POWER UPS//
    public GameObject bloodPotion;
    public GameObject extendedMagazine;
    public GameObject quickHands;
    public GameObject juggernaut;

    //Container variables//
    public int health;
    private int current_health;

    //Visuals//
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Sprite container;
    public Sprite containerBreak1;
    public Sprite containerBreak2;

    //Collider//
    private BoxCollider2D boxCollider;
    private LayerMask aliveLayer;
    private LayerMask deadLayer;

    //Sound//
    private AudioSource audioSource;
    public AudioClip containerBreakSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        animator.enabled = false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        boxCollider = GetComponent<BoxCollider2D>();

        current_health = health;

        //Collider//
        aliveLayer = LayerMask.NameToLayer("Alive");
        deadLayer = LayerMask.NameToLayer("Dead");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break()
    {
        animator.enabled = true;
        int randomPowerUp = Random.Range(1, 100);
        if (randomPowerUp >= 66)
        {
            Instantiate(bloodPotion, transform.position, transform.rotation);
        }

        else if (randomPowerUp >= 33)
        {
            Instantiate(extendedMagazine, transform.position, transform.rotation);
        }

        else if (randomPowerUp >= 0)
        {
            Instantiate(quickHands, transform.position, transform.rotation);
        }

        //if (randomPowerUp >= 0)
        //{
        //    Instantiate(juggernaut, transform.position, transform.rotation);
        //}
    }

    public void GetHit()
    {
        current_health -= 1;
        
        if (current_health == 2)
        {
            spriteRenderer.sprite = containerBreak1;
        }
        else if (current_health == 1)
        {
            spriteRenderer.sprite = containerBreak2;
        }
        else
        {
            Break();
            audioSource.PlayOneShot(containerBreakSound, 0.6f);
            gameObject.layer = deadLayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "map_limit")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        gameObject.layer = aliveLayer;
        
        if (animator != null)
        animator.enabled = false;

        if (spriteRenderer != null)
        spriteRenderer.sprite = container;

        current_health = health;
    }
}
