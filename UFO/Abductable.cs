using UnityEngine;

public class Abductable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player;
    public bool isFlying = false;
    public bool frenar = false;
    public float speed = 30f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = referenceManager.playerReference.GetComponent<Transform>();
    }

    void Update()
    {
        
        if (isFlying == true)
        {
            fly();
            frenar = true;
        }

        if (isFlying == false && frenar == true)
        {
            rb.isKinematic = false;
            frenar = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        //gravity//

        rb.AddForce(0, -9.8f, 0);
    }
    public void fly()
    {
        var direction = (player.position - gameObject.transform.position).normalized;
        rb.isKinematic = true;
        Vector3 newPosition = rb.position + new Vector3(0f, speed * Time.fixedDeltaTime, 0f);
        rb.MovePosition(newPosition);

    }

}
