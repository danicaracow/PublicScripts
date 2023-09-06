using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public List<AudioClip> clipList = new List<AudioClip>();

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayPistolShot()
    {
        _audioSource.PlayOneShot(clipList[0], 0.125f);
    }

    public void PlayHurtSound()
    {
        int soundSelection = Random.Range(0, 2);
        if(soundSelection == 0)
            _audioSource.PlayOneShot(clipList[1], 1f);
        else
            _audioSource.PlayOneShot(clipList[2], 1f);
    }

    public void PlayPistolReloadSound()
    {
        _audioSource.PlayOneShot(clipList[3], 0.4f);
    }

    public void PlayDeadSound()
    {
        _audioSource.PlayOneShot(clipList[4], 1f);
    }

    public void PlayPotionSound()
    {
        _audioSource.PlayOneShot(clipList[5], 1f);
    }

    public void PlayPowerUpSound()
    {
        _audioSource.PlayOneShot(clipList[6], 1f);
    }
}
