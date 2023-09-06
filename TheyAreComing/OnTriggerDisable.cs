using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDisable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "map_limit")
        {
            gameObject.SetActive(false);
        }
    }
}
