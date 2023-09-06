using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_timer : MonoBehaviour
{
    private float timer = 5;
    private float current_timer;

    // Start is called before the first frame update
    void Start()
    {
        current_timer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        current_timer -= Time.deltaTime;

        if (current_timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
