using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    private AudioSource audioSource;
    public bool playSound = false;
    private float fadeLength = 0.2f;
    private float maxVolume = 0.6f;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void fadeInSound()
    {
        StopAllCoroutines();
        StartCoroutine(fadeIn());
    }

    public void fadeOutSound()
    {
        StopAllCoroutines();
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeIn()
    {
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += maxVolume * Time.deltaTime / fadeLength;

            yield return null;
        }

        audioSource.volume = maxVolume;
    }

    IEnumerator fadeOut()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= maxVolume * Time.deltaTime / fadeLength;

            yield return null;
        }

        audioSource.Stop();
    }
}
