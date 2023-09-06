using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plutonio : MonoBehaviour
{
    public float disapear_waiter;
    private float alpha_value;
    public float disapear_speed;
    private Color plutonio_color;
    public GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        plutonio_color = mesh.GetComponent<Renderer>().material.color;
        alpha_value = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (disapear_waiter <= 0)
        {
            Vanish();
            disapear_waiter = 0;
        }

        else
        {
            disapear_waiter -= Time.deltaTime;
        }

    }

    public void Vanish()
    {
        var plutonio_renderer = mesh.GetComponent<Renderer>();
        plutonio_color.a = alpha_value;
        plutonio_renderer.material.color = plutonio_color;


        alpha_value -= disapear_speed * Time.deltaTime;
        if (alpha_value <= 0)
        {
            Destroy(gameObject);
        }
    }
}
