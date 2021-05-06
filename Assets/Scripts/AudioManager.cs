﻿using System;

using UnityEngine.SceneManagement;
using UnityEngine;

// Code referenced: https://www.youtube.com/watch?v=6OT43pvUyfY
//
//
//

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    [SerializeField] AudioSource[] sound3D;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Play("Theme");
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }

        if (sound.source.isPlaying) return;

        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }

        sound.source.Stop();
    }

    public void StopAll()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Stop();
        }
    }
    
    public void ToggleAudio(bool mute)
    {
        foreach (Sound sound in sounds)
        {
            sound.source.mute = mute;
        }
    }
}
