using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public delegate void SFXDamage(SoundGenere type);
    public static SFXDamage PlaySFX;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    private void OnEnable()
    {
        PlaySFX += Play_SFX;
        this.audioSource.volume = SoundManager.SoundVolume;
    }

    private void OnDisable()
    {
        PlaySFX -= Play_SFX;
    }

    private void Play_SFX(SoundGenere type)
    {
        audioSource.PlayOneShot(SoundManager.GetClip(type));
    }
}
