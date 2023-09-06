using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUFO : MonoBehaviour
{
    private AudioSource audioSource;
    private float soundWaiter = 10f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundWaiter >= 0)
        {
            soundWaiter -= Time.deltaTime;
        }
        
        if (soundWaiter <= 0 && audioSource.volume <= 1)
        {
            audioSource.volume += Time.deltaTime * 0.5f;
        }
    }
}
