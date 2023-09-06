using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float destroyTimer;
    private float current_destroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        current_destroyTimer = destroyTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_destroyTimer > 0)
        {
            current_destroyTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
