using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_shot : MonoBehaviour
{
    public float shotForce;
    public GameObject shell_prefab;
    public Enemy_destroyable reference;
    public tank_detection isInRange;
    public bool hasShooted;
    private AudioSource audioSource;
    public AudioClip shot_sound;

    // Start is called before the first frame update
    void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange.shot_range == true && hasShooted == false && reference.isDead == false && reference.isSpawning == false && referenceManager.playerisalive == true)
        {
            Shot_tank_shell();

            hasShooted = true;

            StartCoroutine(ReloadingTime());
        }

    }

    void Shot_tank_shell()
    {
        GameObject shell = Instantiate(shell_prefab, transform.position, transform.rotation);
        Rigidbody rb = shell.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*shotForce, ForceMode.VelocityChange);
        audioSource.pitch = Random.Range(0.8f, 1);
        audioSource.PlayOneShot(shot_sound);
    }

    IEnumerator ReloadingTime()
    {
        yield return new WaitForSeconds(3);

        hasShooted = false;
    }
}
