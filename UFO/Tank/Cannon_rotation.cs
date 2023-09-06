using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_rotation : MonoBehaviour
{
    public tank_detection tank_detection;
    public float speed;
    public GameObject playerReference;
    public Enemy_destroyable enemyStatus;


    // Start is called before the first frame update
    void Start()
    {
        playerReference = referenceManager.playerReference;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStatus.isDead == false && enemyStatus.isSpawning == false && tank_detection.player_detected == true && referenceManager.playerisalive == true)
        {
            Rotacion();
        }
        


    }


    public void Rotacion()
    {
        Vector3 playerPos = (playerReference.transform.position - transform.position).normalized;
        Vector3 lookPos = playerPos + new Vector3(0, 0.05f, 0);
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }
}
