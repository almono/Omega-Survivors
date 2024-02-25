using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController instance;
    public Toggle[] toggles; // collection of settings checkboxes

    public bool damageNumbers = true;
    public bool enemyFlash = true;
    public bool showPickupRange = true;

    [Header("Volume control")]
    public AudioMixer gameAudio;
    public Slider[] volumeSliders;
    public TMP_Text[] volumeSliderText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetVolumeSliderText();

        // Iterate through each Toggle and subscribe to its onValueChanged event
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((value) => OnToggleValueChanged(toggle, value));
        }

        foreach (Slider slider in volumeSliders)
        {
            slider.onValueChanged.AddListener((value) => OnToggleValueChanged(slider, value));
        }
    }

    // Method to handle the Toggle value change
    void OnToggleValueChanged(Toggle toggle, bool toggleState)
    {
        switch(toggle.name)
        {
            case "DamageNumbersToggle":
                damageNumbers = toggleState;
                break;
            case "EnemyFlash":
                enemyFlash = toggleState;
                break;
            case "PickupRange":
                showPickupRange = toggleState;
                break;
            default:
                break;
        }
    }

    void OnToggleValueChanged(Slider slider, float volumeValue)
    {
        float volume = Mathf.Log10(volumeValue) * 20;

        switch (slider.name)
        {
            case "MasterValue":
                gameAudio.SetFloat("MasterVolume", volume);
                volumeSliderText[0].text = Mathf.RoundToInt(volumeSliders[0].value * 100).ToString();
                break;
            case "MusicValue":
                gameAudio.SetFloat("MusicVolume", volume);
                volumeSliderText[1].text = Mathf.RoundToInt(volumeSliders[1].value * 100).ToString();
                break;
            case "SFXValue":
                gameAudio.SetFloat("SFXVolume", volume);
                volumeSliderText[2].text = Mathf.RoundToInt(volumeSliders[2].value * 100).ToString();
                break;
            default:
                break;
        }
    }

    void SetVolumeSliderText()
    {
        volumeSliderText[0].text = Mathf.RoundToInt(volumeSliders[0].value * 100).ToString();
        volumeSliderText[1].text = Mathf.RoundToInt(volumeSliders[1].value * 100).ToString();
        volumeSliderText[2].text = Mathf.RoundToInt(volumeSliders[2].value * 100).ToString();
    }
}
