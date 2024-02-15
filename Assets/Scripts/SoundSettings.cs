using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour
{
    private static readonly string mainMenuPref = "mainMenuPref";
    private static readonly string backgroundPref = "backgroundPref";
    private float mainMenuFloat, backgroundFloat;
    public AudioSource mainMenuAudioSource;
    public AudioSource backgroundAudioSource;
    private void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        mainMenuFloat = PlayerPrefs.GetFloat(mainMenuPref);
        backgroundFloat = PlayerPrefs.GetFloat(backgroundPref);
        mainMenuAudioSource.volume = mainMenuFloat;
        backgroundAudioSource.volume = backgroundFloat;
    }
}
