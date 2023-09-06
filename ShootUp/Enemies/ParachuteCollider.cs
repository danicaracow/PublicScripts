using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteCollider : MonoBehaviour
{

    public GameObject parentObject;
    private Parachute parachute;
    private Parachute_white parachuteWhite;

    // Start is called before the first frame update
    void Start()
    {
        parachute = parentObject.GetComponent<Parachute>();
        parachuteWhite = parentObject.GetComponent<Parachute_white>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (parachute != null)
        {
            parachute.ParachuteHit();
            Destroy(gameObject);
        }
        if (parachuteWhite != null)
        {
            parachuteWhite.ParachuteHit();
            Destroy(gameObject);
        }


    }

    public void DestroyParachute()
    {
        if (parachute != null)
        {
            parachute.ParachuteHit();
            Destroy(gameObject);
        }
        
        if (parachuteWhite != null)
        {
            parachuteWhite.ParachuteHit();
            Destroy(gameObject);
        }
    }

}
