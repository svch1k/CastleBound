using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool Instance;
    public AudioSource AudioSourcePrefab;
    private List<AudioSource> audioSources = new();

    private Dictionary<AudioClipGroup, float> timeouts = new();

    public void Awake()
    {
        Instance = this;
    }

    public bool CanPlay(AudioClipGroup group)
    {
        if (!timeouts.ContainsKey(group))
        {
            timeouts[group] = Time.time;
            return true;
        }
        if (timeouts[group] <= Time.time)
        {
            timeouts[group] = Time.time + group.Cooldown;
            return true;
        }
        return false;
    }

    public AudioSource GetAudioSource()
    {
        foreach (AudioSource src in audioSources)
        {
            if (!src.isPlaying)
            {
                return src;
            }
        }
        AudioSource source = Instantiate<AudioSource>(AudioSourcePrefab, transform);
        audioSources.Add(source);
        return source;
    }

    public void Play(AudioClipGroup group)
    {
        if (!CanPlay(group)) return;
        AudioSource source = GetAudioSource();
        source.spatialBlend = 0.0f;
        group.Play(source);
    }
    public void Play(AudioClipGroup group, Vector2 pos)
    {
        if (!CanPlay(group)) return;
        AudioSource source = GetAudioSource();
        source.transform.position = new Vector3(pos.x, pos.y, 0);
        source.spatialBlend = 1.0f;
        group.Play(source);
    }
}
