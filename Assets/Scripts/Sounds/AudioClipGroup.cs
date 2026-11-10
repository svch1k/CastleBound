using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio Clip Group")]
public class AudioClipGroup : ScriptableObject
{
    public List<AudioClip> Audios;

    public AudioMixerGroup mixerGroup;

    [UnityEngine.Range(0, 2)]
    public float VolumeMin = 1.0f;
    [UnityEngine.Range(0, 2)]
    public float VolumeMax = 1.0f;
    [UnityEngine.Range(0, 2)]
    public float PitchMin = 1.0f;
    [UnityEngine.Range(0, 2)]
    public float PitchMax = 1.0f;
    [UnityEngine.Range(0, 2)]
    public float Cooldown = 0.1f;

    public void Play(AudioSource src)
    {
        src.clip = Audios[Random.Range(0, Audios.Count)];
        src.volume = Random.Range(VolumeMin, VolumeMax);
        src.pitch = Random.Range(PitchMin, PitchMax);
        src.outputAudioMixerGroup = mixerGroup;
        src.Play();
    }
}
