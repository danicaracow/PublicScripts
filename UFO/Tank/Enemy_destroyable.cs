using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_destroyable : MonoBehaviour
{

    //Health and damage//
    public float totalhp = 0;
    public float currenthp;
    public bool hit = false;
    public float colorintensity;
    public GameObject playerReference;
    public PlayerVariables playervariables;

    //Death//
    public float explosionradius = 5f;
    public float explosionforce = 500f;
    public bool isDead = false;
    private float disapearWaiter = 0f;
    private float vanishSpeed = 1f;
    public Enemy_destroyable_parts reference;
    private SoundManager soundManager;
    public bool justOnce = false;
    public GameObject tank;
    public GameObject projectile_spawner;
    public GameObject tracks_R;
    public GameObject tracks_L;
    private SpawnManager spawnManager;
    public float special_bonus;
    public GameObject minimap_sprite;
    public GameObject explosionEffect;

    //Spawn//
    public bool isSpawning = true;
    public float spawnTimer = 0f;
    int LayerGateCollision = 8;

    //Item drop//
    public GameObject plutonio_green;
    public GameObject plutonio_red;

    //Score//
    private HUDManager hudManager;

    [SerializeField] AudioClip[] tank_sounds;
    AudioClip tank_explosion;

    private void Awake()
    {
        //soundManager = FindObjectOfType<SoundManager>();
        
    }


    void Start()
    {

        currenthp = totalhp;
        //int LayerGateCollision = LayerMask.NameToLayer("Tank_Spawned");
        //playerReference = referenceManager.playerReference;
        playervariables = referenceManager.playerReference.GetComponent<PlayerVariables>();
        spawnManager = GameObject.FindGameObjectWithTag("manager").GetComponent<SpawnManager>();

        //sounds//
        tank_explosion = tank_sounds[0];

        //score//
        hudManager = GameObject.FindGameObjectWithTag("manager").GetComponent<HUDManager>();

    }
     
    // Update is called once per frame
    void Update()
    {
        //Once the tank is out of the spawner it collides with the gates
        spawnTimer += 1 * Time.deltaTime;
        if (spawnTimer >= 8)
        {
            isSpawning = false;
            gameObject.layer = LayerGateCollision;

        }

        

        GetDamaged();

        if (currenthp <= 0 && isDead == false)
        {
            EnemyDies();

            // Regenerates special shot if it is not activated //
            

            DropItem();

            GetComponent<AudioSource>().PlayOneShot(tank_explosion);
        }

        if (isDead == true)
        {
            Vanish();
        }

        //si la explosion no se ejecuta después del 'detachment', no surtirá efecto
        if (justOnce == false && reference.detached == true)
        {
            if (!justOnce)
            {
                explosion();
            }
            justOnce = true;
        }

        if (justOnce == true)
        {
            gameObject.GetComponent<Rigidbody>().mass = 0.05f;
        }

    }

    void GetDamaged()
    {

        //Recover from damage when not hit//
        Renderer objectcolor = gameObject.GetComponent<Renderer>();
        if (hit == false)
        {
            if (currenthp > 0.01)
            {
                currenthp += 1 * Time.deltaTime;
            }
            if (currenthp >= totalhp)
            {
                currenthp = totalhp;
            }


        }

        //Gets damaged when hit//
        if (hit == true && isDead == false)
        {
            {

                currenthp -= playervariables.current_damage * Time.deltaTime;

            }
        }

        colorintensity = 1 - (currenthp / totalhp);
        objectcolor.material.color = new Color(1f, 1f - colorintensity, 1f - colorintensity);

        hit = false;
    }

    void EnemyDies()
    {
        isDead = true;
        transform.DetachChildren();
        spawnManager.enemyKilled += 1;
        hudManager.GainScore();
        hudManager.multiplier += 1;
        hudManager.current_multiplier_timer = hudManager.multiplier_timer;
        Destroy(minimap_sprite);
        if (playervariables.specialActivated == false)
        {
            playervariables.current_special_ammo += special_bonus;
        }

    }

    void explosion()
    {

             Collider[] enemyparts = Physics.OverlapSphere(transform.position, explosionradius);
        foreach (Collider part in enemyparts)
        {
            Rigidbody rb = part.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionforce, transform.position, explosionradius);
            }
        }

        Instantiate(explosionEffect, gameObject.transform.position + new Vector3(0, 1f, 0), gameObject.transform.rotation);

    }

    void Vanish()
    {
        Renderer objectcolor = gameObject.GetComponent<Renderer>();
        disapearWaiter += 1 * Time.deltaTime;
        if(disapearWaiter >= 3)
        {
            vanishSpeed -= 0.2f * Time.deltaTime;
            objectcolor.material.color = new Color(1f, 0f, 0f, vanishSpeed);
        }
        
        if(objectcolor.material.color.a <= 0)
        {
            Destroy(gameObject);
            Destroy(tank);
            Destroy(projectile_spawner);
            Destroy(tracks_R);
            Destroy(tracks_L);
        }
    }

    void DropItem()
    {
        int drop_probability = Random.Range(0, 99);
        if (drop_probability <= 35)
        {
            int drop_green_red_probability = Random.Range(0, 99);
            if (drop_green_red_probability <= 80)
            {
                Instantiate(plutonio_green, gameObject.transform.position, gameObject.transform.rotation);
            }
            if (drop_green_red_probability > 80)
            {
                Instantiate(plutonio_red, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
        
    }
}
