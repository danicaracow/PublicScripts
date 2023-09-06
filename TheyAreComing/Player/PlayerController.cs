using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player Variables//
    public int lives;
    private int livesLeft;
    public float speed;
    public float pistolFireSpeed;
    private float current_pistolFireSpeed;
    private bool isAlive = true;

    //Lives//
    public LifeManager lifeManager;

    //Damage received//
    public float EnemyDamage;
    public float ObstacleDamage;
    public float BlubbergeistExplosionDamage;
    public float SpitDamage;

    //Invencibility//
    private bool invencibility;
    public float invencibilityTimer;
    private float current_invencibilityTimer;
    private bool rendererActivated;
    public float blinkingSpeed;
    private float current_blinkingSpeed;

    private Collider2D playerCollider;
    public SpriteRenderer spriteRendererBody;
    public SpriteRenderer spriteRendererLegs;
    

    //Animator//
    public Animator BodyAnimator;

    //Shooting//
    public GameObject bulletSpawner;
    public GameObject bulletPrefab;

    //Object Pool//
    public GameObject BulletsPool;
    public int bulletPoolSize;        
    private GameObject[] bullets;
    private int shootNumber = -1;

    //Reloading//
    public int pistolMagazine;
    private int current_pistolMagazine;
    public float reloadSpeed;
    private float current_reloadSpeed;
    private bool pistolReloadSound_justOnce;
    

    //Sounds//
    public AudioManager audioManager;


    //Map limits//
    private float topLimit = 4f;
    private float bottomLimit = -4.5f;
    private float leftLimit = -11f;
    private float rightLimit = 2f;

    //Screen manager//
    public PauseManager screenManager;

    //UI//
    public GameObject reloadBarBackground;
    public Image reloadBar;
    public Camera mainCamera;
    //BLOOD BAR//
    public Image bloodBar;
    public float bloodBarCapacity;
    public static float current_bloodBarCapacity;
    public float bloodBarSpeed;
    public float bloodBarSpeedIncrease;
    public float maxBloodBarSpeed;
    //UI BULLETS//
    private List<GameObject> bulletList = new List<GameObject>();
    private int bulletNumber;

    public GameObject bullet;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    public GameObject bullet5;
    public GameObject bullet6;
    public GameObject bullet7;
    public GameObject bullet8;
    public GameObject bullet9;

    public Animator UIBulletAnimator;


    //JOYSTICK//
    public FixedJoystick joystick;


    // Start is called before the first frame update
    void Start()
    {
        current_pistolFireSpeed = pistolFireSpeed;
        livesLeft = lives;
        
        //Invencibility
        playerCollider = GetComponent<Collider2D>();
        current_invencibilityTimer = invencibilityTimer;
        current_blinkingSpeed = blinkingSpeed;

        //Reload
        current_pistolMagazine = pistolMagazine;
        current_reloadSpeed = reloadSpeed;
        pistolReloadSound_justOnce = true;
        reloadBarBackground.SetActive(false);

        //BLOOD BAR//
        current_bloodBarCapacity = bloodBarCapacity;

        //UI BULLETS//
        bulletList.AddRange(new GameObject[] {bullet, bullet1, bullet2, bullet3, bullet4, bullet5, bullet6, bullet7, bullet8, bullet9});


        for (int i = 0; i < pistolMagazine; i++)
        {
            bulletList[i].SetActive(true);
        }

        //Object Pool//
        bullets = new GameObject[bulletPoolSize];
        
        for (int i = 0; i < bulletPoolSize; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
            bullets[i].transform.parent = BulletsPool.transform;
            bullets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //PC Player movement and map limits//
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Joystick Player Movement//
        //float horizontalInput = joystick.Horizontal;
        //float verticalInput = joystick.Vertical;

        if (isAlive)
        {
            if (transform.position.y <= topLimit && transform.position.y >= bottomLimit && transform.position.x <= rightLimit && transform.position.x >= leftLimit)
            {
                Vector3 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

                transform.position = newPosition;
            }

            //Clamp position if limit is exceeded//
            if (transform.position.y > topLimit)
                transform.position = new Vector3(transform.position.x, topLimit, transform.position.z);
            if (transform.position.y < bottomLimit)
                transform.position = new Vector3(transform.position.x, bottomLimit, transform.position.z);
            if (transform.position.x > rightLimit)
                transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
            if (transform.position.x < leftLimit)
                transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
        }







        //PC Fire and Reload//

        if (isAlive)
        {
            if (PauseManager.isPaused == false)
            {
                if (current_pistolMagazine > 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Fire();
                        current_pistolFireSpeed = pistolFireSpeed;
                    }
                }
                else
                {
                    Reload();
                }
            }
            

            //if (current_pistolMagazine < pistolMagazine && Input.GetButtonDown("Reload"))
            //{
            //    current_pistolMagazine = 0;
            //}
        }
        else if (reloadBarBackground == true)
        {
            reloadBarBackground.SetActive(false);
        }


        //Reload bar position//
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 1f));
        reloadBarBackground.transform.position = screenPos;

        //BLOOD BAR//
        if (isAlive)
        {
            if (current_bloodBarCapacity > 0)
            {
                current_bloodBarCapacity -= Time.deltaTime * bloodBarSpeed;
                bloodBar.fillAmount = current_bloodBarCapacity / bloodBarCapacity;
            }
            else
                PlayerDead();

            if (current_bloodBarCapacity > bloodBarCapacity)
            {
                current_bloodBarCapacity = bloodBarCapacity;
            }
        }
        

        //Invencibility//
        if (invencibility)
        {
            Invencibility();
        }
        else
        {
            NoInvencibility();
        }

        ///// TO BE IMPLEMENTED /////
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    lifeManager.GainOneLife();
        //    Debug.Log("Vida ganada!");
        //    livesLeft += 1;
        //}

        //DEAD//
        if (!isAlive)
        {
            spriteRendererLegs.enabled = false;
        }

    }


    //ACTIONS//
    private void OnEnable()
    {
        Actions.OnIncreaseDificulty += IncreaseBloodBarSpeed;
    }

    private void OnDisable()
    {
        Actions.OnIncreaseDificulty -= IncreaseBloodBarSpeed;
    }


    public void Fire()
    {
        BodyAnimator.Play("player_pistol_shot", -1, 0f);
        current_pistolMagazine -= 1;
        shootNumber++;
        audioManager.PlayPistolShot();

        // Check if we exceeded array's range
        if (shootNumber >= bulletPoolSize)
        {
            shootNumber = 0;
        }

        // Bullet placement and activation
        bullets[shootNumber].transform.position = bulletSpawner.transform.position;
        bullets[shootNumber].SetActive(true);

        //UI BULLET//
        bulletList[current_pistolMagazine].SetActive(false);

        UIBulletAnimator.Play("bullet_fire", -1, 0f);
    }

    public void Reload()
    {
        
        if (current_reloadSpeed <= 0)  //When player finishes reloading...
        {
            current_pistolMagazine = pistolMagazine;
            current_reloadSpeed = reloadSpeed;
            pistolReloadSound_justOnce = true;
            reloadBarBackground.SetActive(false);

            for (int i = 0; i < pistolMagazine; i++)
            {
                bulletList[i].SetActive(true);
            }
        }
        else  //During reloading...
        {
            reloadBarBackground.SetActive(true);
            reloadBar.fillAmount = (reloadSpeed - current_reloadSpeed) / reloadSpeed;
            current_reloadSpeed -= Time.deltaTime;
            if (pistolReloadSound_justOnce)
            {
                audioManager.PlayPistolReloadSound();
                pistolReloadSound_justOnce = false;
            }
            
        }
        
    }

    public void PistolNextBullet()
    {
        current_pistolFireSpeed -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive)
        {
            if (collision.transform.tag != "blubbergeist_detection")
            {
                //ENEMIES//
                if (collision.transform.tag == "enemy")
                {
                    GetHit(EnemyDamage);

                    BodyCollider enemyBody;
                    HeadCollider enemyHead;

                    enemyBody = collision.GetComponent<BodyCollider>();
                    enemyHead = collision.GetComponent<HeadCollider>();

                    invencibility = true;
                    //LifeCheck();
                }

                else if (collision.transform.tag == "obstacle")
                {
                    GetHit(ObstacleDamage);
                    invencibility = true;
                    //LifeCheck();
                }

                else if (collision.transform.tag == "blubbergeist_explosion")
                {
                    GetHit(BlubbergeistExplosionDamage);
                    invencibility = true;
                    //LifeCheck();
                }

                else if (collision.transform.tag == "bullet")
                {
                    GetHit(SpitDamage);
                    invencibility = true;
                    //LifeCheck();
                }

                //POWER UPS//
                else if (collision.transform.tag == "blood_potion")
                {
                    current_bloodBarCapacity += 50;
                    PlayPotionSound();
                    Actions.OnPickUp();
                    Destroy(collision.gameObject);
                }

                else if (collision.transform.tag == "extended_magazine")
                {
                    if (pistolMagazine < 10)
                    {
                        pistolMagazine += 1;
                        if (current_pistolMagazine != 0)
                        {
                            current_pistolMagazine += 1;
                            bulletList[current_pistolMagazine - 1].SetActive(true);
                        }
                        PlayPowerUpSound();
                        Actions.OnPickUp();
                        Destroy(collision.gameObject);
                    }
                    
                }

                else if (collision.transform.tag == "quick_hands")
                {
                    if (reloadSpeed > 1)
                    {
                        reloadSpeed -= 0.1f;
                        //current_reloadSpeed = reloadSpeed;
                        PlayPowerUpSound();
                        Actions.OnPickUp();
                        Destroy(collision.gameObject);
                    }
                    
                }
            }
        }  
    }

    public void PlayerDead()
    {
        isAlive = false;
        BodyAnimator.Play("player_dead_anim", -1, 0f);
        audioManager.PlayDeadSound();
        //Play.OneShot(playerDeadSound)
        GetComponent<ConstantMovement>().enabled = true;
        screenManager.DisplayDeadScreen();
    }

    public void Invencibility()
    {
        //Disable collider//
        playerCollider.enabled = false;

        //Invencibility countdown and check if it is 0 to disable it//
        current_invencibilityTimer -= Time.deltaTime;
        if (current_invencibilityTimer <= 0)
        {
            invencibility = false;
        }

        //Blinking effect//
        current_blinkingSpeed -= Time.deltaTime;

        if (current_blinkingSpeed <= 0)
        {
            rendererActivated = !rendererActivated;
            current_blinkingSpeed = blinkingSpeed;
        }

        if (rendererActivated == false)
        {
            spriteRendererBody.enabled = false;
            spriteRendererLegs.enabled = false;
        }
        else
        {
            spriteRendererBody.enabled = true;
            spriteRendererLegs.enabled = true;
        }
            



    }

    public void NoInvencibility()
    {
        spriteRendererBody.enabled = true;
        spriteRendererLegs.enabled = true;
        current_invencibilityTimer = invencibilityTimer;
        playerCollider.enabled = true;
    }

    public void GetHit(float damage)
    {
        //livesLeft -= 1;
        //lifeManager.LoseOneLife();
        audioManager.PlayHurtSound();
        current_bloodBarCapacity -= damage;
    }

    public void PlayPowerUpSound()
    {
        audioManager.PlayPowerUpSound();
    }

    public void PlayPotionSound()
    {
        audioManager.PlayPotionSound();
    }

    private void LifeCheck()
    {
        if (livesLeft <= 0)
        {
            PlayerDead();
        }
        else
        {
            //Activate invencibility//
            invencibility = true;
        }
    }


    private void IncreaseBloodBarSpeed()
    {
        if (bloodBarSpeed < maxBloodBarSpeed)
        bloodBarSpeed += bloodBarSpeedIncrease;
    }



    public void MobileFire()
    {
        if (isAlive)
        {
            if (current_pistolMagazine > 0)
            {
                
                Fire();
                
            }
            else
            {
                Reload();
            }

            if (current_pistolMagazine < pistolMagazine && Input.GetButtonDown("Reload"))
            {
                current_pistolMagazine = 0;
            }
        }
    }
}
