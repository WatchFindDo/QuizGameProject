using System;
using UnityEngine;

[System.Serializable()]
public struct SoundParameters
{
    [Range(0, 1)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;
    public bool Loop;
}
[System.Serializable()]
public class Sound
{
    #region Variables

    [SerializeField]    String              name            = String.Empty;
    public              String              Name            { get { return name; } }

    [SerializeField]    AudioClip           clip            = null;
    public              AudioClip           Clip            { get { return clip; } }

    [SerializeField]    SoundParameters     parameters      = new SoundParameters();
    public              SoundParameters     Parameters      { get { return parameters; } }

    [HideInInspector]
    public              AudioSource         Source          = null;

    #endregion

    public void Play ()
    {
        Source.clip = Clip;

        Source.volume = Parameters.Volume;
        Source.pitch = Parameters.Pitch;
        Source.loop = Parameters.Loop;

        Source.Play();
    }
    public void Stop ()
    {
        Source.Stop();
    }
}
public class AudioManager : MonoBehaviour {

    #region Variables

    public static       AudioManager    Instance        = null;

    [SerializeField]    Sound[]         sounds          = null;
    [SerializeField]    AudioSource     sourcePrefab    = null;

    [SerializeField]    String          startupTrack    = String.Empty;

    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        { Destroy(gameObject); }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        InitSounds();
    }
    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        if (string.IsNullOrEmpty(startupTrack) != true)
        {
            PlaySound(startupTrack);
        }
    }

    #endregion

    /// <summary>
    /// Function that is called to initializes sounds.
    /// </summary>
    void InitSounds()
    {
        foreach (var sound in sounds)
        {
            AudioSource source = (AudioSource)Instantiate(sourcePrefab, gameObject.transform);
            source.name = sound.Name;

            sound.Source = source;
        }
    }

    /// <summary>
    /// Function that is called to play a sound.
    /// </summary>
    public void PlaySound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.Play();
        }
        else
        {
            Debug.LogWarning("Sound by the name " + name + " is not found! Issues occured at AudioManager.PlaySound()");
        }
    }
    /// <summary>
    /// Function that is called to stop a playing sound.
    /// </summary>
    public void StopSound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.Stop();
        }
        else
        {
            Debug.LogWarning("Sound by the name " + name + " is not found! Issues occured at AudioManager.StopSound()");
        }
    }

    #region Getters

    Sound GetSound(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.Name == name)
            {
                return sound;
            }
        }
        return null;
    }

    #endregion
}