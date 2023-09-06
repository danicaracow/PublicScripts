using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_destroyable_parts : MonoBehaviour
{

    public Enemy_destroyable reference;
    public bool hit = false;
    public bool scriptIsEnabled = true;
    private bool justOnce = false;
    //la variable 'detached' es necesaria para confirmar cuando estan las partes del tanque separadas. Una vez separadas, esta variable se vuelve true y activa la explosion en 'Enemy_destroyable'
    public bool detached = false;
    private float disapearWaiter = 0f;
    private float vanishSpeed = 1f;

    void Start()
    {

    }

    void Update()
    {


        GetHit();


        if (reference.isDead == true)
        {
            if (justOnce == false)
            {
                Death();
                justOnce = true;
            }

            Vanish();

        }


    }

    void Vanish()
    {
        Renderer objectcolor = gameObject.GetComponent<Renderer>();
        disapearWaiter += 1 * Time.deltaTime;
        if (disapearWaiter >= 3)
        {
            vanishSpeed -= 0.2f * Time.deltaTime;
            objectcolor.material.color = new Color(1f, 0f, 0f, vanishSpeed);
        }
        if (vanishSpeed <= 0)
        {
            Destroy(gameObject);
        }
    }



    void GetHit()
    {
        if(hit == true)
        {
            reference.hit = true;
        }

        Renderer objectcolor = gameObject.GetComponent<Renderer>();
        float colorintensity = reference.colorintensity;
        ; objectcolor.material.color = new Color(1f, 1f - colorintensity, 1f - colorintensity);

    }

    void DetachParts()
    {

        transform.DetachChildren();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        detached = true;
    }

    void DisableScripts()
    {

            scriptIsEnabled = false;

    }

    void Death()
    {
        DetachParts();
        DisableScripts();
        StartCoroutine (MassDecreaseWaiter());
    }

    IEnumerator MassDecreaseWaiter()
    {
        yield return null;
        gameObject.GetComponent<Rigidbody>().mass = 0.05f;
        
    }
}
