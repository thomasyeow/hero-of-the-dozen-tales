using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SoundGenere { MAIN_MENU, BACKGROUND, HIT}

public class SoundManager : MonoBehaviour
{
    public delegate void SoundMangerEvent(SoundGenere type);
    public static SoundMangerEvent SetGenereEvent;

    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    private AudioSource audioSource;

    [SerializeField] private List<SoundClipSO> clips;
    private AudioClip activeClip;

    private SoundGenere activeState;
    private IEnumerator activeCouroutine;

    public static float SoundVolume { get; set; }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        SoundVolume = 0.5f;
    }

    private void OnEnable()
    {
        activeState = SoundGenere.MAIN_MENU;
        SetGenereEvent += SetGenere;

        if (activeCouroutine == null)
        {
            activeClip = clips[0].SoundClip;
            activeCouroutine = FadeIn(audioSource,0.01f, activeClip);
            StartCoroutine(MainLoop());
        }
    }

    private IEnumerator MainLoop()
    {
        while (true)
        {
            SetClip(activeState);
            yield return new WaitForSeconds(activeClip.length);
            activeClip = clips[Random.Range(0, clips.Count - 1)].SoundClip;
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime, AudioClip audioToPlay)
    {
     
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();


        StartCoroutine(FadeIn(audioSource, 2f, audioToPlay));

    }

    IEnumerator FadeIn(AudioSource audioSource, float FadeTime, AudioClip audioToPlay)
    {
        audioSource.volume = SoundVolume;
        audioSource.clip = audioToPlay;
        audioSource.Play();
        while (audioSource.volume < audioSource.volume)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    public void SetClip(AudioClip audio)
    {
        UpdateActiveCoroutine(FadeOut(this.audioSource, 1.5f, audio));
    }

    public void SetClip(SoundGenere type)
    {
        var clips = this.clips.Where(x => x.SoundType == type).ToList();
        if (clips != null)
            SetClip(clips[Random.Range(0, clips.Count)].SoundClip);
    }

    public void SetClipNoFade(AudioClip audio)
    {
        audioSource.Stop();
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void PlayClip(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    public void PlayClip(SoundGenere type)
    {
        var clips = this.clips.Where(x => x.SoundType == type).ToList();
        if (clips != null)
            PlayClip(clips[Random.Range(0, clips.Count)].SoundClip);
    }

    private void UpdateActiveCoroutine(IEnumerator newCoroutine)
    {
        StopCoroutine(activeCouroutine);
        activeCouroutine = newCoroutine;
        StartCoroutine(activeCouroutine);
    }

    public static AudioClip GetClip(SoundGenere type)
    {
        var clips = SoundManager.Instance.clips.Where(x => x.SoundType == type).ToList();
        return clips != null ? clips[Random.Range(0, clips.Count - 1)].SoundClip : null;
    }

    public void SetVolume(float volume)
    {
        SoundVolume = volume;
        this.audioSource.volume = volume;
    }

   private void SetGenere(SoundGenere type)
    {
        activeState = type;
    }
}


