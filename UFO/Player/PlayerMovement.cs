using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;


    public float Force = 1000f;
    public float DashForce = 5000f;
    public float RotationVelocity = 10f;
    private bool isDashing = false;
    private float dashingTimer = 2f;
    private Transform rot;
    private float player_velocity;
    public AudioSource player_movement_sound;
    public float factor = 0.1f;

    private void Start()
    {
        player_movement_sound.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sound//
        
        player_velocity = rb.velocity.magnitude;
        player_movement_sound.pitch = 1f + player_velocity * factor;

        if (referenceManager.playerisalive == false) 
        {
            player_movement_sound.Stop();
        }

        //MOVEMENT//

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputVector = new Vector3(h, 0, v);
        Vector3 direction = Vector3.Normalize(inputVector);


        if (referenceManager.playerisalive == true)
        {


            //Movement//
            if (Input.GetKey("w") | Input.GetKey("s") | Input.GetKey("a") | Input.GetKey("d"))
            {

                    rb.AddRelativeForce(direction * Force * Time.deltaTime);

                
                //if (Input.GetKey("a"))
                //{
                //    rb.AddRelativeForce(-Force/ 4 * Time.deltaTime, 0, Force/ 4 * Time.deltaTime);
                //}

                //if (Input.GetKey("d"))
                //{
                //    rb.AddRelativeForce(Force / 4 * Time.deltaTime, 0, Force / 4 * Time.deltaTime);
                //}

            }
            /*
            if (Input.GetKey("s"))
            {

                    rb.AddRelativeForce(direction * Force * Time.deltaTime);

            }

            if (Input.GetKey("a"))
            {

                    rb.AddRelativeForce(direction * Force * Time.deltaTime);

            }

            if (Input.GetKey("d"))
            {

                    rb.AddRelativeForce(direction * Force * Time.deltaTime);


            }

            */

            //Rotation//
            if (Input.GetKey("q"))
            {
                transform.Rotate(0, -RotationVelocity * Time.deltaTime, 0);
            }
            if (Input.GetKey("e"))
            {
                transform.Rotate(0, RotationVelocity * Time.deltaTime, 0);
            }


        }

            
    }
}
