using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Reflection;
using static Yields;

public enum AudioType
{
    BGM,
    SFX,

    Length
}

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioMixer _audioMixer;
    // public AudioMixer AudioMixer => _audioMixer;

    [Header("BGM For each Scene")]
    [SerializeField]
    private List<AudioClip> _bgms; // BuildingScene 순서로 BGM 배치


	private Dictionary<string, AudioClip> _bgmDict = new Dictionary<string, AudioClip>();

    private Dictionary<AudioType, Action<AudioClip>> _audioActionDictionary = new();
    private AudioSource[] _audioSources = new AudioSource[(int)AudioType.Length];

    private void Awake()
    {
        InitializeChildObject();
        InitializePlayMethod();
        Play(AudioType.BGM, _bgms[SceneManager.GetActiveScene().buildIndex]);
    }

    private void InitializeChildObject()
    {
        for (int i = 0; i < (int)AudioType.Length; ++i)
        {
            GameObject g = new GameObject(((AudioType)i).ToString());
            g.transform.SetParent(transform);
            _audioSources[i] = g.AddComponent<AudioSource>();
            _audioSources[i].playOnAwake = false;
            _audioSources[i].outputAudioMixerGroup = _audioMixer.FindMatchingGroups(((AudioType)i).ToString())[0];
        }
        
        _audioSources[(int)AudioType.BGM].loop = true;
    }

    private void InitializePlayMethod()
    {
        _audioActionDictionary.Add(AudioType.BGM,PlayerBGM);
        _audioActionDictionary[AudioType.BGM] += PlayBGM;
        _audioActionDictionary[AudioType.SFX] += PlaySFX;
    }

    private void PlayBGM(AudioClip clip)
    {
        if(clip == null)
        {
            Debug.Log("Clip is Null");
            return;
        }
        AudioSource audioSource = _audioSources[(int)AudioType.BGM];
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void PlaySFX(AudioClip clip)
    {
        if(clip == null)
        {
            Debug.Log("Clip is Null");
            return;
        }

        AudioSource audioSource = _audioSources[(int)AudioType.SFX];
        audioSource.PlayOneShot(clip);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        Play(AudioType.BGM,_bgmDict[scene.name]);
    }

    public void Play(AudioType type, AudioClip clip)
    {
        _audioActionDictionary[type]?.Invoke(clip);
    }
	public void StopBGM()
	{
		AudioSource audioSource = _audioSources[(int)AudioType.BGM];
		if (audioSource.isPlaying)
			audioSource.Stop();
	}
	public void SetPitch(AudioType type, float pitch)
    {
        _audioSources[(int)type].pitch = pitch;
    }

    public float GetPitch(AudioType type)
    {
        return _audioSources[(int)type].pitch;
    }

    public void SetAllSourcePitch(float pitch)
    {
        foreach (AudioSource source in _audioSources)
        {
            source.pitch = pitch;
        }
    }
    
}
