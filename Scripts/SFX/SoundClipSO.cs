using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Clip", menuName = "Sound System/New clip")]
public class SoundClipSO : ScriptableObject
{
    [SerializeField] private SoundGenere soundType;
    public SoundGenere SoundType => soundType;
    [SerializeField] private AudioClip soundClip;
    public AudioClip SoundClip => soundClip;
}
