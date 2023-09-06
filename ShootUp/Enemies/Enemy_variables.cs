using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_variables : MonoBehaviour
{
    public int health;
    public int currentHealth;
    public float speed;
    public GameObject piecesPrefab1;
    public GameObject piecesPrefab2;
    public GameObject piecesPrefab3;
    public GameObject piecesPrefab4;

    //Parachute//
    public bool dropParachute;
    [SerializeField]
    private bool isDropping;
    public int parachuteNumber;
    public GameObject parachutePrefab;
    public GameObject parachute_redPrefab;
    public GameObject parachute_whitePrefab;
    public GameObject paratrooper_spawner;
    [SerializeField]
    private float parachuteTimer;
    public float maxParachuteTimer;
    public float minParachuteTimer;
    public float parachuteIntervalTimer;
    private float current_parachuteIntervalTimer = 0f;


    //Destroy timer
    public float destroyTimer;
    private float current_destroyTimer;
    private Enemy_spawn_manager spawn_manager;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        current_destroyTimer = destroyTimer;
        parachuteTimer = Random.Range(minParachuteTimer, maxParachuteTimer);
        spawn_manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Enemy_spawn_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();


        if (currentHealth <= 0)
        {
            EnemyDies();
        }

        if (dropParachute == true)
        {
            if (isDropping == false)
            {
                if (parachuteTimer > 0)
                {
                    parachuteTimer -= Time.deltaTime;
                }
                else
                {
                    isDropping = true;
                }
            }
            else
            {
                DropParachute();
            }
        }

        if (current_destroyTimer >= 0)
        {
            current_destroyTimer -= Time.deltaTime * System.Math.Abs(speed);
        }
        else
        {
            if (transform.tag == "plane")
            {
                spawn_manager.current_planes -= 1;
            }
            if (transform.tag == "small_plane")
            {
                spawn_manager.current_smallPlanes -= 1;
            }
            if (transform.tag == "ally_plane")
            {
                spawn_manager.current_allyPlanes -= 1;
            }

            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "bullet")
        {
            currentHealth -= 1;
        }
    }


    private void EnemyDies()
    {
        Destroy(gameObject);
        if (transform.tag == "plane")
        {
            GameObject piece = Instantiate(piecesPrefab1, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), transform.rotation);
            piece.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * 150f, 0f));
            GameObject piece2 = Instantiate(piecesPrefab2, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), transform.rotation);
            piece2.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * 150f, 0f));
            GameObject piece3 = Instantiate(piecesPrefab3, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), transform.rotation);
            piece3.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * 150f, 0f));
            GameObject piece4 = Instantiate(piecesPrefab4, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), transform.rotation);
            piece4.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * 150f, 0f));
            spawn_manager.current_planes -= 1;
        }
        else if (transform.tag == "small_plane")
        {
            spawn_manager.current_smallPlanes -= 1;
        }
        else
        {
            spawn_manager.current_allyPlanes -= 1;
        }
    }

    private void Movement()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }

    private void DropParachute()
    {
        if (parachuteNumber > 0)
        {
            current_parachuteIntervalTimer -= Time.deltaTime;

            
                if (current_parachuteIntervalTimer <= 0)
                {
                    int random = Random.Range(0, 2);
                    if (random == 1)
                    {
                        if (transform.tag == "plane" | transform.tag == "small_plane")
                        {
                            Instantiate(parachutePrefab, paratrooper_spawner.transform.position, parachutePrefab.transform.rotation);
                        }
                        if (transform.tag == "ally_plane")
                        {
                            Instantiate(parachute_whitePrefab, paratrooper_spawner.transform.position, parachutePrefab.transform.rotation);
                        }
                
                    }
                    
                    else
                    {
                        if (transform.tag == "plane" | transform.tag == "small_plane")
                        {
                            Instantiate(parachute_redPrefab, paratrooper_spawner.transform.position, parachutePrefab.transform.rotation);
                        }
                        if (transform.tag == "ally_plane")
                        {
                            Instantiate(parachute_whitePrefab, paratrooper_spawner.transform.position, parachutePrefab.transform.rotation);
                        }
                    }

                    parachuteNumber -= 1;
                    current_parachuteIntervalTimer = parachuteIntervalTimer + Random.Range(0, 2f);
                    
                }
            
            
        }
    }
}
