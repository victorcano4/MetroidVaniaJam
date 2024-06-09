using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options_menu : MonoBehaviour
{

    // Public reference to the Audio Mixer
    public AudioMixer audioMixer;

    // Public reference to the Slider
    public Slider volumeSlider;



    void OnEnable()
    {
        // Add listener for when the slider value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void OnDisable()
    {
        // Remove listener when the script is disabled
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    // Method to set the volume
    public void SetVolume(float sliderValue)
    {
        // Convert the slider value to dB
        float volume = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("MasterVolume", volume);
    }

}
