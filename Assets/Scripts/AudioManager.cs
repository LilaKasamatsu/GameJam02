using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    private AudioSource source;

    public AudioMixerGroup audioMixerGroup;

    public string clipName;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,3f)]
    public float pitch;

    public bool loop = false;
    public bool playOnAwake = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;
    }

    public void Play()
    {
        source.PlayOneShot(clip);
    }

    public void Stop()
    {
        source.Stop();
    }
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;
    List<AudioSource> sources;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sources = new List<AudioSource>();
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].clipName);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            sources.Add(_go.GetComponent<AudioSource>());
        }

        PlaySound("Main Music");
        PlaySound("Trail Sound");
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].clipName == name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].clipName == name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void MuteSource(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].clipName == name)
            {
                sounds[i].volume = 0;
                return;
            }
        }
    }

    public void ChangePitch(float pitch, string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].clipName == name)
            {
                sounds[i].pitch = pitch;
                sources[i].pitch = pitch;
                return;
            }
        }
    }

    public void ChangeVolume(float volume, string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].clipName == name)
            {
                sounds[i].volume = volume;
                sources[i].volume = volume;
                return;
            }
        }
    }
}
