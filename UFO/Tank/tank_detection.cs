using UnityEngine;

public class tank_detection : MonoBehaviour
{
    public float detectionDistance;
    private float distanceFromPlayer;
    public GameObject playerReference;
    public tank_movement chasis;
    public Enemy_destroyable reference;
    public bool shot_range = false;
    public bool player_detected = false;

    private void Start()
    {
        playerReference = referenceManager.playerReference;
    }
    void FixedUpdate()

    {
        if (reference.isSpawning == true && reference.isDead == false)
        {
            chasis.Movement_spawn();
        }
        
        if (reference.isDead == false && reference.isSpawning == false && referenceManager.playerisalive == true)
        {
 

            PlayerDetection();
        }
        


    }

    void PlayerDetection()
    {
        //calculate distance from player//
        distanceFromPlayer = (transform.position - playerReference.transform.position).magnitude;

        if (reference.isDead == false)
        {
            if (distanceFromPlayer <= detectionDistance)
            {
                shot_range = true;
                player_detected = true;
            }
            else
            {
                chasis.Movement();
                chasis.Rotacion();
                player_detected = true;
                shot_range = false;

            }
        }
        




        //if (reference.isDead == false && shot_range == false && distanceFromPlayer >= detectionDistance)
        //{

            //Vector3 playerPos = (playerReference.transform.position - transform.position).normalized;
            //RaycastHit hit;

            //if (Physics.Raycast(gameObject.transform.position, playerPos, out hit, detectionDistance))
            //{
            //if (distanceFromPlayer >= detectionDistance / 2)
            //{
            //chasis.Movement();
            //chasis.Rotacion();
            //player_detected = true;
            //shot_range = false;

            //}


        //}
                //Debug.DrawRay(gameObject.transform.position, playerPos * detectionDistance, Color.green);
                //Debug.DrawRay(gameObject.transform.position, playerPos * detectionDistance / 2, Color.red);
            //}
        //if (distanceFromPlayer <= detectionDistance)
        //{
                //chasis.Stop();
        //        shot_range = true;
        //        player_detected = true;
        //}
    
        
    }
}
