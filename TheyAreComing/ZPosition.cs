using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPosition : MonoBehaviour
{
    public float zMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float newYPosition = transform.position.y; // Get the current Y position
        float newZPosition = newYPosition * zMultiplier; // Calculate the new Z position as a function of Y

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZPosition);
        transform.position = newPosition; // Update the object's position with the new Z value

    }
}
