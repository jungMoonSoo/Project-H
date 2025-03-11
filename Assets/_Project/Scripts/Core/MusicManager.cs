using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audIntro;
    [SerializeField] private AudioSource audLoop;
    
    [Header("Music Settings")]
    [SerializeField] private MusicInfo[] audMusic;
    [SerializeField] private MusicName selectedMusic = MusicName.None;
    
    
    private MusicName currentMusic = MusicName.None;
    private float loopWaitTime = 0;
    private Dictionary<MusicName, MusicInfo> musics = new Dictionary<MusicName, MusicInfo>();

    
    void Start()
    {
        foreach (MusicInfo music in audMusic)
        {
            musics.Add(music.musicName, music);
        }
        
        PlayMusic(selectedMusic);
    }

    void OnValidate()
    {
        PlayMusic(selectedMusic);
    }

    public async Awaitable PlayMusic(MusicName musicName)
    {
        if (musicName == currentMusic) return;
        
        StopCoroutine(nameof(PlayLoopCo));
        
        audIntro.Stop();
        audLoop.Stop();
        if (musics.TryGetValue(musicName, out MusicInfo music))
        {
            audLoop.clip = music.loopClip;
            currentMusic = musicName;
            
            if (music.introClip is not null)
            {
                audIntro.clip = music.introClip;
                audIntro.Play();
                
                loopWaitTime = music.loopStartTime;
                StartCoroutine(nameof(PlayLoopCo));
            }
            else
            {
                audLoop.Play();
            }
        }
    }

    private IEnumerator PlayLoopCo()
    {
        yield return new WaitForSeconds(loopWaitTime);
        
        audLoop.Play();
    }
}

[Serializable]
public class MusicInfo
{
    public MusicName musicName;
    
    public AudioClip introClip;
    public AudioClip loopClip;

    public float loopStartTime = -1;
}

public enum MusicName
{
    None,
    KingdomOfHailish,
    SupremeChaos
}