using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    [SerializeField]
    private AudioSource rocketEngineAudioSource;

    [SerializeField]
    private AudioSource musicAudioSource;
    private float fadeLength = 0.2f;
    private float maxRocketEngineVolume = 0.6f;

    void Start()
    {
        setVolumes();
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
        rocketEngineAudioSource.volume = 0;
        rocketEngineAudioSource.Play();

        while (rocketEngineAudioSource.volume < maxRocketEngineVolume)
        {
            rocketEngineAudioSource.volume += maxRocketEngineVolume * Time.deltaTime / fadeLength;

            yield return null;
        }

        rocketEngineAudioSource.volume = maxRocketEngineVolume;
    }

    IEnumerator fadeOut()
    {
        while (rocketEngineAudioSource.volume > 0)
        {
            rocketEngineAudioSource.volume -= maxRocketEngineVolume * Time.deltaTime / fadeLength;

            yield return null;
        }

        rocketEngineAudioSource.Stop();
    }

    public void setVolumes()
    {
        musicAudioSource.volume = PlayerPrefs.GetFloat("music_volume", 0.6f);
        maxRocketEngineVolume = PlayerPrefs.GetFloat("sfx_volume", 0.6f);
    }
}
