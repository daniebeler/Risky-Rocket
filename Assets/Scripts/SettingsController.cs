using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private Slider MusicSlider;

    [SerializeField]
    private Slider SfxSlider;

    [SerializeField]
    private SoundController soundController;

    void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("music_volume", 0.6f);
        SfxSlider.value = PlayerPrefs.GetFloat("sfx_volume", 0.6f);

        MusicSlider.onValueChanged.AddListener(delegate { setPlayerPref("music_volume", MusicSlider.value); });
        SfxSlider.onValueChanged.AddListener(delegate { setPlayerPref("sfx_volume", SfxSlider.value); });
    }

    private void setPlayerPref(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        soundController.setVolumes();
    }
}
