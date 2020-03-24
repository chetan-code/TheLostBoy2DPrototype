using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAudioManager : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
