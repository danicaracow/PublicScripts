using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_script : MonoBehaviour
{

    public bool hit_player = false;
    private bool hit_all = false;
    private float disapearWaiter = 0f;
    private float vanishSpeed = 1f;
    void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == "Player" && hit_player == false)
        {
            hit_player = true;
            if (referenceManager.playerReference.GetComponent<PlayerVariables>().shieldIsActivated == false)
            {
                
                player.gameObject.GetComponent<PlayerVariables>().GetDamaged(5);
                player.gameObject.GetComponent<Player_sounds>().ShellHitSound();
            }
            else
            {
                player.gameObject.GetComponent<Player_sounds>().ShieldHitSound();
            }
            
        }
        hit_all = true;

        if (player.gameObject.tag == "Shield")
        {
            referenceManager.playerReference.gameObject.GetComponent<Player_sounds>().ShieldHitSound();
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit_player == true | hit_all == true)
        {
            Vanish();
        }
        
    }

    void Vanish()
    {
        Renderer objectcolor = gameObject.GetComponent<Renderer>();
        disapearWaiter += 1 * Time.deltaTime;
        if (disapearWaiter >= 1)
        {
            vanishSpeed -= 0.4f * Time.deltaTime;
            objectcolor.material.color = new Color(1f, 1f, 1f, vanishSpeed);
        }

        if (objectcolor.material.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}

