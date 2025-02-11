using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicSource;
    public AudioSource[] soundLoops;
    public AudioSource[] soundSources;

    private Queue<AudioSource> _queueSources;

    public AudioClip[] gunShoots;
    public AudioClip[] btn;

    public override void Awake()
    {
        base.Awake();
        _queueSources = new Queue<AudioSource>(soundSources);
        OnInit();
    }
    public void OnInit()
    {
        ChangeSoundVolume();
        ChangeMusicVolume();
    }

    public void ChangeSoundVolume()
    {
        foreach (var sound in soundSources)
        {
         //   sound.volume = DataPersist.VolumeSound;
        }
        foreach (var sound in soundLoops)
        {
            //sound.volume = UserData.FxSound;
            //sound.mute = !UserData.IsSoundOn;
        }
    }
    public void ChangeMusicVolume(float volume)
    {
        if(musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
    public void ChangeSoundVolume(float volume)
    {
        foreach (var sound in soundSources)
        {
            sound.volume = volume;
        }
    }

    public void ChangeMusicVolume()
    {
        if (musicSource != null)
        {
            //musicSource.volume = DataPersist.VolumeMusic;
        }
    }

    public void PlayShot(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            return;
        }
        AudioSource source = _queueSources.Dequeue();
        if (!source)
        {
            return;
        }
        //source.volume = volume;
        source.PlayOneShot(clip);
        _queueSources.Enqueue(source);
    }

    public void StopLoop(string name)
    {
        foreach (var sound in soundLoops)
        {
            if (sound.name.Equals(name))
            {
                sound.Stop();
            }
        }
    }
    public void PlayLoop(string name)
    {
        foreach (var sound in soundLoops)
        {
            if (sound.name.Equals(name))
            {
                sound.Play();
            }
        }
    }
}