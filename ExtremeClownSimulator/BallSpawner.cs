using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public float dropTimer;
    public float current_dropTimer;

    //Ball Prefabs//
    public GameObject ballPrefab1;
    public GameObject ballPrefab2;
    public GameObject ballPrefab3;
    public GameObject ballPrefab4;
    private List<GameObject> ballList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        current_dropTimer = dropTimer;
        ballList.Add(ballPrefab1);
        ballList.Add(ballPrefab2);
        ballList.Add(ballPrefab3);
        ballList.Add(ballPrefab4);

    }

    // Update is called once per frame
    void Update()
    {
        if (current_dropTimer <= 0)
        {
            DropBall();
        }
        else
        {
            current_dropTimer -= Time.deltaTime;
        }




    }

    public void DropBall()
    {
        Instantiate(ballList[Random.Range(0, ballList.Count)], transform.position, transform.rotation);
        current_dropTimer = dropTimer;
    }
}
