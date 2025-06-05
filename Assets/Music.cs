using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip levelMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = SoundEffects.instance.PlayLoopSFXClip(levelMusic, this.transform);
        audioSource.volume = 0.6f;
    }
    
}
