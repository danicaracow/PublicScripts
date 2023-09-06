using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float totalhp = 0;
    public float currenthp;
    public bool hit = false;
    public float colorintensity;
    PlayerVariables playerVariables;
    public Renderer rd;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        currenthp = totalhp;
        playerVariables = referenceManager.playerReference.GetComponent<PlayerVariables>();

    }

    // Update is called once per frame
    void Update()
    {


        Hitteable();
        

        if (currenthp <= 0 | SpawnManager.RoundFinished == true)
        {
            Explode();

        }

    }

    public void Explode()
    {

            Instantiate(explosion, gameObject.transform.position, explosion.transform.rotation);
            Destroy(gameObject);

    }

    public void Hitteable()
    {
        if (hit == false)
        {
            if (currenthp > 0.01)
            {
                currenthp += 1 * Time.deltaTime;
            }
            if (currenthp >= totalhp)
            {
                currenthp = totalhp;
            }


        }

        if (hit == true)
        {
            {

                currenthp -= playerVariables.current_damage * Time.deltaTime;

            }
        }

        colorintensity = 1 - (currenthp / totalhp);
        rd.material.color = new Color(1f, 1f - colorintensity, 1f - colorintensity);

        hit = false;
    }
}
