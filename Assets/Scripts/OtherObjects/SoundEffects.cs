using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;

    [SerializeField] private AudioSource sfxObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audio, Transform transform)
    {
        AudioSource source = Instantiate(sfxObject, transform.position,Quaternion.identity);

        source.clip = audio;
        source.volume = Settings.gamesvolume;
        source.Play();

        float cliplength = source.clip.length;

        Destroy(source.gameObject,cliplength);
    }

    public AudioSource PlayLoopSFXClip(AudioClip audio, Transform transform)
    {
        AudioSource source = Instantiate(sfxObject, transform.position, Quaternion.identity);

        source.loop = true;
        source.clip = audio;
        source.volume = Settings.gamesvolume;
        source.Play();

      

        return source;
    }
}
