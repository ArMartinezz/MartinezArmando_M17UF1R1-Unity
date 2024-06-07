using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound {
    public string clipName;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public Sound[] sfxSounds, musicSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    public void PlayMusic(string name) 
    {
        Sound s = Array.Find(musicSounds, x => x.clipName == name);

        if (s == null) Debug.Log("Sound not found");
        else 
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.clipName == name);

        if (s == null) Debug.Log("Sound not found");
        else 
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
