using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public bool isParalax;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (!isParalax)
            speed = WorldManager.groundSpeed;
        else
            speed = WorldManager.backgroundSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (transform.tag == "obstacle" || transform.tag == "prompt")
    //    {
    //        if (collision.transform.tag == "map_limit")
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

}
