using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timer = 3f;
    private float current_timer;
    // Start is called before the first frame update
    void Start()
    {
        current_timer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_timer > 0)
        {
            current_timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
