using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfxjump;
    [SerializeField] AudioSource sfxCoin;
    [SerializeField] AudioSource sfxCrash;
    [SerializeField] AudioSource sfxEagle;

    public bool IsMute { get => bgm.mute ;}

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgm.isPlaying)
            bgm.Stop();

        bgm.clip= clip;
        bgm.loop = loop;
        bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if(sfxjump.isPlaying)
            sfxjump.Stop();

        sfxjump.clip= clip;
        sfxjump.Play();
    }

    public void PlaySFXcoin(AudioClip clip)
    {
        if (sfxCoin.isPlaying)
            sfxCoin.Stop();

        sfxCoin.clip = clip;
        sfxCoin.Play();
    }
    public void PlaySFXCrash(AudioClip clip)
    {
        if (sfxCrash.isPlaying)
            sfxCrash.Stop();

        sfxCrash.clip = clip;
        sfxCrash.Play();
    }
    public void PlaySFXEagle(AudioClip clip)
    {
        if (sfxEagle.isPlaying)
            sfxEagle.Stop();

        sfxEagle.clip = clip;
        sfxEagle.Play();
    }
    public void SetMute(bool Value)
    {
        bgm.mute = Value;
    }
}
