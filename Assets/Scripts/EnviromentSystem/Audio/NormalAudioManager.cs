using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAudioManager : MonoBehaviour
{
    static NormalAudioManager current;
    [Header("环境声音")]
    public AudioClip ambientClip;
    public AudioClip musicClip;

    [Header("音效")]
    public AudioClip[] walkStepClips;
    public AudioClip[] attackClips;
    public AudioClip jumpClip;
    public AudioClip dashClip;

    public AudioClip jumoVoiceClip;

    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource fxSource;
    AudioSource playerSource;
    AudioSource voiceSource;

    private void Awake()
    {
        current = this;

        DontDestroyOnLoad(this);

        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }

    public static void PlayFootstepAudio()
    {
        int index = Random.Range(0, current.walkStepClips.Length);

        current.playerSource.clip = current.walkStepClips[index];
        current.playerSource.Play();
    }

    public static void PlayAttackAudio()
    {
        //int index = Random.Range(0, current.attackClips.Length);
        foreach(var index in current.attackClips)
        {
            current.playerSource.clip = index;
            current.playerSource.Play();
        }
    }

    public static void PlayDashAudio()
    {
        current.playerSource.clip = current.dashClip;
        current.playerSource.Play();
    }

    void StartLevelAudio()
    {
        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
    }
}
