using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abudction_trigger : MonoBehaviour
{
    public ambience_sounds playerCamera;
    public PlayerVariables playerVariables;
    private Transform player;
    void OnTriggerEnter(Collider object_interaction)
    {
        if (object_interaction.gameObject.tag == "plutonio_green" && Input.GetMouseButton(1))
        {
            Destroy(object_interaction.gameObject);
            playerVariables.plutonio_green_counter += 1;
            playerCamera.PlayTakePlutonio();
        }

        if (object_interaction.gameObject.tag == "plutonio_red" && Input.GetMouseButton(1))
        {
            Destroy(object_interaction.gameObject);
            playerVariables.plutonio_red_counter += 1;
            playerCamera.PlayTakePlutonio();
        }

    }

    private void Update()
    {
        player = referenceManager.playerReference.GetComponent<Transform>();
        gameObject.transform.position = player.position;
    }


}
