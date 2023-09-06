using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_spawner : MonoBehaviour
{
    public GameObject rocket_prefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Spawn()
    {
        //Instantiate Tank//
        Instantiate(rocket_prefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
