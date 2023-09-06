using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_controller : MonoBehaviour
{
    Animator anim;
    public Tank_Spawner tank_spawner;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (tank_spawner.isSpawning == true)
        {
            anim.SetBool("Open", true);
        }
        else
        {
            anim.SetBool("Open", false);
        }



    }
}
