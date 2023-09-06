using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandController : MonoBehaviour
{
    public bool isActivated;
    public bool isHolding;
    private GameObject ballInTrigger;
    private GameObject ballBeingHold;
    private Rigidbody2D rb_ballBeingHold;
    private BallManager ballManager_ballInTrigger;
    public GameManager manager;

    //Player Controls//
    public string rightHandKey;
    public int player;

    //Ball List
    List<GameObject> ballList = new List<GameObject>();


    //Ball Throw
    public GameObject throwPoint;
    public float throwSpeed;
    private Vector3 throwDirection;

    //Ball Break
    public float bounceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        throwDirection = (throwPoint.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballList.Count > 0)
        {
            if (isHolding == false && Input.GetKeyDown(rightHandKey))
            {
                GetBall(player);
            }

        }


        if (isHolding == true && Input.GetKeyUp(rightHandKey))
        {
            ThrowBall();
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "ball")// | collision.transform.tag == "ball_thrown")
        {
            ballBeingHold = collision.gameObject;

            //ballManager_ballInTrigger = collision.GetComponent<BallManager>();
            if (isHolding == false) //Si no tienes ya una bola en la mano
            {
                ballList.Add(collision.gameObject);
                //ballManager_ballInTrigger = ballList[0].GetComponent<BallManager>();
                Debug.Log("Ball added to list");
            }
            else //if (ballManager_ballInTrigger.isBroken == false) //Si ya tienes una bola en la mano
            {
                //ballManager_ballInTrigger.isBroken = true;
                collision.gameObject.layer = LayerMask.NameToLayer("Ball_noCollision");
                collision.GetComponent<Rigidbody2D>().AddForce(Vector3.up * bounceSpeed);
                //ballList.RemoveAt(0);
                Debug.Log("BOUNCE");
                //Destroy(ballBeingHold);
                //isHolding = false;
                if (player == 1)
                {
                    manager.FalloP1();
                }
                else
                {
                    manager.FalloP2();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "ball")
        {
            //if (ballManager_ballInTrigger.isBroken == false)
            //{
                if (ballList.Count != 0)
                {
                    //if (ballManager_ballInTrigger.isThrownByLeftHand == false)
                    //{
                        ballList.RemoveAt(0);
                        Debug.Log("Ball removed from list");
                //}
                    if (player == 1)
                    {
                        manager.FalloP1();
                    }
                    else
                    {
                        manager.FalloP2();
                    }

            }
           //}

        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "ball")
    //    {
    //        if (isHolding == false)
    //        {
    //            if (ballInTrigger == null)
    //            {
    //                isActivated = true;
    //                ballManager_ballInTrigger = ballInTrigger.GetComponent<BallManager>();
    //                if(ballManager_ballInTrigger.isThrownByLeftHand == false)
    //                {
    //                    ballInTrigger = collision.gameObject;
    //                }
    //            }

    //        }
    //        //else
    //        //{
    //        //    Destroy(collision.gameObject);
    //        //    Destroy(ballBeingHold);
    //        //    isHolding = false;
    //        //}


    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "ball")
    //    {
    //        if (isHolding == true)
    //        {
    //            Destroy(collision.gameObject);
    //            Destroy(ballBeingHold);
    //            isHolding = false;
    //        }

    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "ball")
    //        isActivated = false;
    //    ballInTrigger = null;
    //}

    private void GetBall(int player)
    {

        //ballBeingHold = ballList[0];
        rb_ballBeingHold = ballBeingHold.GetComponent<Rigidbody2D>();
        ballBeingHold.transform.position = transform.position;
        rb_ballBeingHold.isKinematic = true;
        rb_ballBeingHold.velocity = Vector3.zero;
        isHolding = true;

        if (player == 1)
        {
            manager.AciertoP1();
        }
        else
        {
            manager.AciertoP2();
        }
    }

    private void ThrowBall()
    {
        rb_ballBeingHold.isKinematic = false;
        rb_ballBeingHold.AddForce(throwDirection * throwSpeed);
        isHolding = false;
        //ballBeingHold.transform.tag = "ball_thrown";
        //ballBeingHold.GetComponent<BallManager>().isThrownByLeftHand = true;
        if (ballList.Count != 0)
        {
            ballList.RemoveAt(0);
            Debug.Log("Ball removed from list");
        }

        //if (ballInTrigger == null)
        //{
        //    isActivated = false;
        //}

    }


}
