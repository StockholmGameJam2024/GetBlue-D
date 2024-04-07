using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio Settings")]
public class AudioSettings : ScriptableObject
{
  [Tooltip("In Percentage"), Range(0.0001f, 1)]
  public float masterVolume = 1;

  [Tooltip("In Percentage"), Range(0.0001f, 1)]
  public float sfxVolume = 1;
  [Tooltip("In Percentage")]
  public float lowPassCutoffFrequency = 5000f;
  public float maxLowPassCutoffFrequency = 22000f;
  
  public AudioMixer audioMixer;
  public AudioMixer sfxMixer;
  
  //One shot audio clips
  public AudioClip startGameButtonAudio;
  public AudioClip clickedButtonAudio;
  public AudioClip hoverButtonAudio;
  public AudioClip winAudio;
  
  //Music audio clips
 
  public AudioClip introMenuMusic;
  public AudioClip menuMusic;
  [Tooltip("Part of the music, but only meant to be played when starting the game")]
  public AudioClip startGameIntroAudio;
  public List<AudioClip> gameMusic;
  public AudioClip creditsMusic;
  

  public void ActivateLowPassFilter()
  {
    //Set the low pass cutoff frequency to 5000 Hz
    audioMixer.SetFloat("mainCutoffFrequency", lowPassCutoffFrequency);
  }
    
  public void DeactivateLowPassFilter()
  {
    //Set the low pass cutoff frequency to 22000 Hz
    audioMixer.SetFloat("mainCutoffFrequency", maxLowPassCutoffFrequency);
  }
  
  public void SetMasterVolume(float volume)
  {
    masterVolume = volume;
    SetMixerVolume();
  }
  
  public int GetVolumeToDisplay()
  {
    return Mathf.RoundToInt(masterVolume*100);
  }

  public void SetMixerVolume()
  {
    audioMixer.SetFloat("masterVolume",  Mathf.Log10(masterVolume) * 20); // 20 because audio math
    sfxMixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);
  }

  public int GetSFXVolumeToDisplay()
  {
    return Mathf.RoundToInt(sfxVolume*100);
  }

  public void SetSFXVolume(float evtNewValue)
  {
    sfxVolume = evtNewValue;
    SetMixerVolume();
  }
}
