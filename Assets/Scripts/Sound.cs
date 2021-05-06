using UnityEngine;

[System.Serializable]

// Code referenced: https://www.youtube.com/watch?v=6OT43pvUyfY
//
//
//
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
