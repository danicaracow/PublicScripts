using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_sounds : MonoBehaviour
{
    public AudioSource getHit;
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip hit4;
    public AudioClip hit5;
    public AudioClip damaged1;
    public AudioClip damaged2;
    public AudioClip damaged3;
    public AudioClip dead;
    public AudioClip dead_explosion;
    public AudioClip shieldHit;
    public AudioClip specialReady;
    private List<AudioClip> hitSoundList = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        getHit = gameObject.AddComponent<AudioSource>();
        hitSoundList.Add(hit1);
        hitSoundList.Add(hit2);
        hitSoundList.Add(hit3);
        hitSoundList.Add(hit4);
        hitSoundList.Add(hit5);
        hitSoundList.Add(damaged1);
        hitSoundList.Add(damaged2);
        hitSoundList.Add(damaged3);
        hitSoundList.Add(dead);
        hitSoundList.Add(dead_explosion);
        hitSoundList.Add(specialReady);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShellHitSound()
    {
        getHit.PlayOneShot(hitSoundList[Random.Range(0, 4)]);
        getHit.PlayOneShot(hitSoundList[Random.Range(5, 7)], 1);
    }
    public void ShieldHitSound()
    {
        getHit.PlayOneShot(shieldHit, 1);
    }

    public void SpecialReadySound()
    {
        getHit.PlayOneShot(specialReady, 1);
    }

    public void PlayDeadSound()
    {
        getHit.PlayOneShot(hitSoundList[8], 1);
        getHit.PlayOneShot(hitSoundList[9], 0.5f);
    }

}
