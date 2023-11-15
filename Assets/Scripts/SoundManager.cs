using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
[System.Serializable]
public class SoundSource
{
    public GameObject linkedObject;
    public AudioClip clip;
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    [Range(10.0f, 300.0f)]
    public float falloffDistance = 200f;
    public bool loop = true;
    //  public float spatialBlend = 1.0f;
    // Reference to the dynamically created AudioSource
    [System.NonSerialized]
    public AudioSource audioSource;
}

public class SoundManager : MonoBehaviour
{
    public List<SoundSource> soundSources = new List<SoundSource>();

    // Singleton pattern
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SoundManager");
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance of SoundManager in the scene
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
     
        // Initialize AudioSource components for each SoundSource
        foreach (SoundSource soundSource in soundSources)
        {
            if (soundSource.linkedObject != null)
            {
                // Create AudioSource and attach it to the linkedObject
                soundSource.audioSource = soundSource.linkedObject.AddComponent<AudioSource>();
                soundSource.audioSource.clip = soundSource.clip;
                soundSource.audioSource.volume = soundSource.volume;
                soundSource.audioSource.loop = soundSource.loop;
                soundSource.audioSource.spatialBlend = 1;// 3D Sounjd
                soundSource.audioSource.spatialize = true; // HRTF
                soundSource.audioSource.maxDistance = soundSource.falloffDistance;

                soundSource.audioSource.Play();
            }
        }
    }

    // Play the sound associated with a SoundSource
    public void PlaySound(SoundSource soundSource)
    {
        if (soundSource != null && soundSource.audioSource != null && soundSource.clip != null)
        {
            soundSource.audioSource.Play();

        }
    }

    // Stop the sound associated with a SoundSource
    public void StopSound(SoundSource soundSource)
    {
        if (soundSource != null && soundSource.audioSource != null)
        {
            soundSource.audioSource.Stop();
        }
    }

    // Adjust the volume of the sound associated with a SoundSource
    public void SetVolume(SoundSource soundSource, float volume)
    {
        if (soundSource != null && soundSource.audioSource != null)
        {
            soundSource.audioSource.volume = Mathf.Clamp01(volume);
        }
    }
}