using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public bool isThrownByLeftHand;
    public bool isThrownByRightHand;
    public bool isBroken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "map_limit")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isThrownByLeftHand && isThrownByRightHand)
        {
            isThrownByLeftHand = false;
            isThrownByRightHand = false;
        }
    }
}
