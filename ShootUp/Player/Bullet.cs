using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float destroyTime = 6f;
    public GameObject impactParticle;
    public GameObject particleLocation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            destroyTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("plane") | collision.transform.CompareTag("small_plane"))
        {
            Instantiate(impactParticle, particleLocation.transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
