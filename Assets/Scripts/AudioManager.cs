using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundManager[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        foreach(SoundManager sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }

    public AudioSource findAudioClip(string name)
    {
        SoundManager soundClip = Array.Find(sounds, sound => sound.name == name);
        return soundClip.audioSource;
    }

    public void playSound(string name)
    {
        SoundManager soundClip = Array.Find(sounds, sound => sound.name == name);
        soundClip.audioSource.Play();
    }
}
