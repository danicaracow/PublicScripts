using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumiMovement : MonoBehaviour
{
    [SerializeField] private float movSpeed;
    [SerializeField] public bool isTargeted;

    // Start is called before the first frame update
    void Start()
    {
        isTargeted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lumiPosition = transform.position;
        transform.position += new Vector3(0f, 1f, 0f) * movSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Map_Limit")
        {
            Destroy(gameObject);
        }

    }

    public void LumiTargeted()
    {
        isTargeted = true;
    }
}
