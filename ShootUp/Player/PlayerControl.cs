using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Player Variables//

    [SerializeField] private bool isAlive = true;
    public static int currentHealth;
    public static int maxHealth = 1000;


    //Movement//
    public GameObject cannon;
    public float rotationSpeed = 10f;
    private float horizontalInput;
    public bool leftPressed;
    public bool rightPressed;

    //Fire//
    public GameObject fire;
    public GameObject bullet;
    public float bulletSpeed;
    private bool reloading;
    public float reloadingTime;
    private float currentReloadingTime;
    public bool firePressed;
    public ParticleSystem shotParticles;
    private Animator cannonAnimator;
    public GameObject cannonSprite;

    void Start()
    {
        currentHealth = maxHealth;
        cannonAnimator = cannonSprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //CannonMovement();

        if (firePressed | Input.GetKey("space"))
        {
            PlayerFire();
        }

        Reload();

        if (leftPressed | Input.GetKey("a"))
        {
            CannonMovementLeft();
        }

        if (rightPressed | Input.GetKey("d"))
        {
            CannonMovementRight();
        }

        if (currentHealth <= 0 && isAlive == true)
        {
            isAlive = false;
        }
    }

    public void CannonMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Get input from A/D keys or left/right arrows
        Quaternion currentRotation = cannon.transform.rotation;
        float angle = cannon.transform.rotation.eulerAngles.z;
        
        //convierto el valor 'angle' para que exprese un número de 0 (izquierda) a 180 (derecha)
        if (angle >= 0 && angle < 179)
            angle = -angle + 90;
        if (angle > 180 && angle < 360)
            angle = -angle + 450;

        if (Input.GetKey("a") && angle > 0f)
        {
            cannon.transform.rotation = currentRotation * Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("d") && angle < 180f)
        {
            cannon.transform.rotation = currentRotation * Quaternion.Euler(0, 0, -rotationSpeed * Time.deltaTime);
        }
        
        //Si la rotación del cañón se pasa unos decimales, corrijo la posición
        if (angle < 0f)
        {
            cannon.transform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        if (angle > 180f)
        {
            cannon.transform.rotation = Quaternion.Euler(0, 0, 270f);
        }
    }
    

    public void CannonMovementLeft()
    {
        Quaternion currentRotation = cannon.transform.rotation;
        float angle = cannon.transform.rotation.eulerAngles.z;
        //convierto el valor 'angle' para que exprese un número de 0 (izquierda) a 180 (derecha)
        if (angle >= 0 && angle < 179)
            angle = -angle + 90;
        if (angle > 180 && angle < 360)
            angle = -angle + 450;

        if (angle > 0f)
        {
            cannon.transform.rotation = currentRotation * Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    public void CannonMovementRight()
    {
        Quaternion currentRotation = cannon.transform.rotation;
        float angle = cannon.transform.rotation.eulerAngles.z;
        
        //convierto el valor 'angle' para que exprese un número de 0 (izquierda) a 180 (derecha)
        if (angle >= 0 && angle < 179)
            angle = -angle + 90;
        if (angle > 180 && angle < 360)
            angle = -angle + 450;
        if (angle < 180f)
        {
            cannon.transform.rotation = currentRotation * Quaternion.Euler(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }

    public void PlayerFire()
    {
        if (reloading == false)
        {
            GameObject bulletInstance = Instantiate(bullet, fire.transform.position, fire.transform.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, bulletSpeed));
            reloading = true;
            currentReloadingTime = reloadingTime;
            shotParticles.Play();
            cannonAnimator.Play("Player_fire", -1, 0f);
        }

    }

    private void Reload()
    {
        if (currentReloadingTime > 0 && reloading == true)
        {
            currentReloadingTime -= Time.deltaTime;
        }
        else
        {
            reloading = false;
            currentReloadingTime = reloadingTime;
        }
    }
}
