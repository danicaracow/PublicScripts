using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class referenceManager : MonoBehaviour
{
    public static referenceManager Instance { get; set; }
    public static GameObject playerReference;
    public static bool playerisalive = true;
    public PlayerVariables playerVariables;

    void Awake()
    {


        //Assign player reference//
        playerReference = GameObject.FindGameObjectWithTag("Player");
        playerisalive = true;

    }

    private void Update()
    {
        if (playerVariables.health <= 0)
        {
            playerisalive = false;
        }
    }





}
