using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_script : MonoBehaviour
{
    private PlayerVariables playerVariables;
    private GameObject playerReference;
    private Vector3 playerPosition;
    private Rigidbody rb;
    public GameObject smoke;
    public GameObject explosion;
    public float rotationSpeed;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = referenceManager.playerReference;
        rb = GetComponent<Rigidbody>();
        playerVariables = referenceManager.playerReference.GetComponent<PlayerVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        //destroy rockets if player dies//
        if (referenceManager.playerisalive == false)
        {
            smoke.AddComponent<destroy_timer>();
            DetachSmoke();
            Instantiate(explosion, gameObject.transform.position, explosion.transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Movement()
    {
        Vector3 playerPos = (playerReference.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(playerPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
        rb.AddRelativeForce(0, 0, movementSpeed * Time.deltaTime);
    }

    public void DetachSmoke()
    {
        if (smoke.transform.parent != null)
        {
            smoke.transform.parent = null;
        }
    }

    void OnCollisionEnter(Collision object_interaction)
    {
        if(object_interaction.gameObject.tag == "Player")
        {
            
            smoke.AddComponent<destroy_timer>();
            DetachSmoke();
            Instantiate(explosion, gameObject.transform.position, explosion.transform.rotation);
            Destroy(gameObject);
            if (playerVariables.shieldIsActivated == false)
            {
                object_interaction.gameObject.GetComponent<PlayerVariables>().GetDamaged(10);
            }
        }
        
        


    }
}

