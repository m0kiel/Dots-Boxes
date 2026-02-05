using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType { CompleteSquare, PlaceLine, ButtonClick, SliderSlide }
public enum MixerGroupType { Master }

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<SoundClipInfo> soundClipInfoList = new();
    private Dictionary<SoundType, SoundClipInfo> soundClips = new();

    [SerializeField] List<MixerGroupInfo> mixerGroupsInfoList = new();
    private Dictionary<MixerGroupType, AudioMixerGroup> mixers = new();

    private void Awake()
    {
        SetMixers();
        SetSoundClips();
    }

    public void PlaySound(SoundType sound)
    {
        audioSource.outputAudioMixerGroup = mixers[soundClips[sound].mixerGroupType];
        audioSource.PlayOneShot(soundClips[sound].sound, soundClips[sound].volume);
    }

    private void SetMixers()
    {
        for (int i = 0; i < mixerGroupsInfoList.Count; i++)
        {
            mixers.Add(mixerGroupsInfoList[i].mixerGroupType, mixerGroupsInfoList[i].mixerGroup);
        }
    }

    private void SetSoundClips()
    {
        for (int i = 0; i < soundClipInfoList.Count; i++)
        {
            soundClips.Add(soundClipInfoList[i].soundType, soundClipInfoList[i]);
        }
    }
}

[Serializable]
public struct SoundClipInfo
{
    public SoundType soundType;
    [Range(0, 1)] public float volume;
    public MixerGroupType mixerGroupType;
    public AudioClip sound;
}

[Serializable]
public struct MixerGroupInfo
{
    public MixerGroupType mixerGroupType;
    public AudioMixerGroup mixerGroup;
}
