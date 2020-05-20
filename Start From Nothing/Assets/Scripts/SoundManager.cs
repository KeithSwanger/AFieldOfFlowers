using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundType { SeedAppears, Powerup, PlantSeed, LaunchPetals, EquipItem}

public class SoundManager : MonoBehaviour
{

    public AudioClip seedAppears;
    public AudioClip powerup;
    public AudioClip plantSeed;
    public AudioClip launchPetals;
    public AudioClip equipItem;

    public AudioSource musicSource;
    public AudioClip music;

    public List<AudioSource> audioSources;

    LinkedList<AudioSource> oldestSources;

    public bool SFXDisabled = false;


    // Start is called before the first frame update
    void Start()
    {

        oldestSources = new LinkedList<AudioSource>();
        foreach (AudioSource availableSource in audioSources)
        {
            oldestSources.AddLast(availableSource);
        }
        

        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(Vector2 location, SoundType soundType)
    {
        if (SFXDisabled)
        {
            return;
        }

        AudioSource audioSource = FindSource();

        audioSource.transform.position = location;
        audioSource.clip = GetClip(soundType);
        audioSource.Stop();
        audioSource.Play();
        
        
    }

    private AudioSource GetOldestSource()
    {
        throw new NotImplementedException();
    }

    AudioSource FindSource()
    {
        AudioSource foundSource = oldestSources.First.Value;
        oldestSources.RemoveFirst();
        oldestSources.AddLast(foundSource);
        return foundSource;
    }

    AudioClip GetClip(SoundType soundType)
    {
        if(soundType == SoundType.SeedAppears)
        {
            return seedAppears;
        }
        else if (soundType == SoundType.Powerup)
        {
            return powerup;
        }
        else if (soundType == SoundType.PlantSeed)
        {
            return plantSeed;
        }
        else if (soundType == SoundType.LaunchPetals)
        {
            return launchPetals;
        }
        else if (soundType == SoundType.EquipItem)
        {
            return equipItem;
        }
        else
        {
            return null;
        }
    }

    public void PlayMusic()
    {
        musicSource.clip = music;
        musicSource.Play();
        musicSource.loop = true;
        
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            PauseMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    public void ToggleSFX()
    {
        if(SFXDisabled == false)
        {
            SFXDisabled = true;
        }
        else
        {
            SFXDisabled = false;
        }
    }
    
}
