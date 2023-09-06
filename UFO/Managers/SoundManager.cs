using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public Sound[] sounds;

    [SerializeField] private AudioClip[] audios;

    private AudioSource controlAudio;


    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();

    }

    public void AudioSelection(int index, float volume)
    {
        controlAudio.PlayOneShot(audios[index], volume);
    }

}
  
