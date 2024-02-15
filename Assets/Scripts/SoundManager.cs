using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static readonly string firstPlay = "firstPlay";
    private static readonly string mainMenuPref = "mainMenuPref";
    private static readonly string backgroundPref = "backgroundPref";
    private int firstPlayInt;
    public Slider mainMenuSlider, backgroundSlider;
    private float mainMenuFloat, backgroundFloat;
    public AudioSource mainMenuAudioSource;
    public AudioSource backgroundAudioSource;

    private void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(firstPlay);

        if (firstPlayInt==0)
        {
            mainMenuFloat = .125f;
            backgroundFloat = .25f;
            mainMenuSlider.value = mainMenuFloat;
            backgroundSlider.value = backgroundFloat;
            PlayerPrefs.SetFloat(mainMenuPref, mainMenuFloat);
            PlayerPrefs.SetFloat(backgroundPref, backgroundFloat);
            PlayerPrefs.SetInt(firstPlay, -1);
        }
        else
        {
            mainMenuFloat = PlayerPrefs.GetFloat(mainMenuPref);
            mainMenuSlider.value = mainMenuFloat;
            backgroundFloat = PlayerPrefs.GetFloat(backgroundPref);
            backgroundSlider.value = backgroundFloat;
        }        
    }
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(mainMenuPref, mainMenuSlider.value);
        PlayerPrefs.SetFloat(backgroundPref, backgroundSlider.value);
    }
    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }
    public void UpdateSound()
    {
        mainMenuAudioSource.volume = mainMenuSlider.value;     
        backgroundAudioSource.volume = backgroundSlider.value;    
    }
}
