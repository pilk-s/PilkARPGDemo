using System;
using System.Collections;
using System.Collections.Generic;
using piiilk_ARPGDemo.Assets;
using UnityEngine;

public enum SoundType
{
    ATK,
    HIT,
    BLOCK,
    FOOT,
}

public class PoolItemSound : PoolItemBase
{
    private AudioSource _audioSource;

    [SerializeField] private SoundType _type;
    [SerializeField] private AssetsSoundSO _soundAssets;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Spawn()
    {
        PlayerSound();
    }

    private void PlayerSound()
    {
        _audioSource.clip = _soundAssets.GetAudioClip(_type);
        _audioSource.Play();
        StartRecycle();
    }

    
    private void StartRecycle()
    {
        TimerManager.MainInstance.TryGetOneTimer(0.3f,DisableSelf);
    }

    private void DisableSelf()
    {
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}