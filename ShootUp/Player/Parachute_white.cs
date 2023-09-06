using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute_white : MonoBehaviour
{
    public float speed;
    public GameObject particlesBlood;
    public Sprite noParachute;
    public bool isFalling;
    public bool isLanded;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //parachute and soldier
    public GameObject parachute;
    public GameObject soldier;
    
    //firing and reloading
    public bool reloading = true;
    public float reloadingTime;
    public float currentReloadingTime;

    //health//
    public int health;

    //Enemy detection//
    RaycastHit2D hitleft;
    RaycastHit2D hitright;
    private int layerMask;
    private float enemyRightDistance;
    private float enemyLeftDistance;
    private GameObject enemyRight;
    private GameObject enemyLeft;
    private GameObject target;
    private bool TargetDetected;
    private Vector2 scale;
    private SoldierCollider targetSoldier;
    private Parachute targetParachute;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentReloadingTime = reloadingTime;
        layerMask = LayerMask.GetMask("paratrooper_enemy");
        scale = transform.localScale;


    }

    // Update is called once per frame
    void Update()
    {
        if (isLanded == false)
        {
            Movement();
        }
        else
        {
            if (target == null)
            {
                EnemyDetection();
            }
            else
            {
                targetParachute = target.GetComponent<SoldierCollider>().parentObject.GetComponent<Parachute>();
            }
            
            //SET TARGET AND LOOK TO HIM//
            if (enemyLeft != null && enemyRight != null)
            {
                transform.localScale = (enemyLeftDistance <= enemyRightDistance) ? scale * new Vector2(-1, 1) : scale;
                target = (enemyLeftDistance <= enemyRightDistance) ? enemyLeft : enemyRight;
            }
            else if (enemyLeft != null)
            {
                transform.localScale = scale * new Vector2(-1, 1);
                target = enemyLeft;
            }
            else if (enemyRight != null)
            {
                transform.localScale = scale;
                target = enemyRight;
            }


            //if (enemyLeft != null && enemyRight != null)
            //{

            //}


            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Aiming_white"))
            {
                Fire();
                Reload();
            }
            
        }

        if (health <= 0)
        {
            EnemyDies();
        }


        Debug.Log(target);
        

    }

    public void EnemyDies()
    {
        Destroy(gameObject);
        Instantiate(particlesBlood, transform.position, transform.rotation);
    }

    public void ParachuteHit()
    {
        //spriteRenderer.sprite = noParachute;
        speed = speed * 2;
        animator.SetBool("isFalling", true);
        isFalling = true;
    }

    public void Movement()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + -speed * Time.deltaTime);
    }

    public void Landing()
    {
        Destroy(parachute);
        //spriteRenderer.sprite = noParachute;
        animator.SetBool("isLanded", true);
        isLanded = true;
    }

    public void Fire()
    {
        if (reloading == false)
        {

            if (PlayerControl.currentHealth > 0)
            {
                if (target != null)
                {
                    if (targetParachute != null)
                    {
                        targetParachute.health -= 1;
                        animator.SetTrigger("isFiring");
                        reloading = true;
                        currentReloadingTime = reloadingTime;
                    }
                    
                }
                
            }

            //animator.SetBool("isFiring", true);

        }

    }

    private void Reload()
    {
        //animator.SetBool("isFiring", false);
        if (currentReloadingTime > 0 && reloading == true)
        {
            currentReloadingTime -= Time.deltaTime;
        }
        else
        {
            reloading = false;
        }
        
    }

    private void EnemyDetection()
    {

            hitleft = Physics2D.Raycast(soldier.transform.position, Vector2.left, Mathf.Infinity, layerMask);
            hitright = Physics2D.Raycast(soldier.transform.position, Vector2.right, Mathf.Infinity, layerMask);

            if (hitleft.collider != null)
            {
                if (hitleft.transform.tag == "soldier_enemy")
                {
                    enemyLeft = hitleft.collider.gameObject;
                    enemyLeftDistance = hitleft.distance;
                    TargetDetected = true;
                }
            }
            else
            {
                //target = null;
                enemyLeft = null;
            }

            if (hitright.collider != null)
            {
                if (hitright.transform.tag == "soldier_enemy")
                {
                    enemyRight = hitright.collider.gameObject;
                    enemyRightDistance = hitright.distance;
                    TargetDetected = true;
                }
            }
            else
            {
                //target = null;
                enemyRight = null;
            }

            if (hitright.collider == null && hitleft.collider == null)
            {
                target = null;
                TargetDetected = false;
            }

        
    }
}
