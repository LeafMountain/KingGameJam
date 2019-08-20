using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager_ref;

    [Header("Audio Sources")]
    public AudioSource mainMenuAudio;
    public AudioSource sfxAudio;
    public AudioSource musicAudio;

    [Header("Menu Sound")]
    public AudioClip[] mainMenuSFX;

    [Header("SFX")]
    public AudioClip[] SFX;

    [Header("MusicClips")]
    public AudioClip[] musicClips;

    private bool isOn;
    private bool defaultIsPlaying;
    private bool beatCount;
    private float beatLength = 0.75f;
    private float timeTracker;
    private int beatsTracker;
    void Awake()
    {
        if(audioManager_ref == null)
        {
            audioManager_ref = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static AudioManager GetInstance()
    {
        return audioManager_ref;
    }
    void Start()
    {
        
    }
    public void StartMusicTrack()
    {
        isOn = true;
        defaultIsPlaying = true;
        musicAudio.clip = musicClips[1];
       
    }
    public void PauseMusicTrack()
    {
        isOn = false;
    }
    public void StopMusicTrack()
    {
        isOn = false;
    }

    public void ButtonSelect()
    {
        mainMenuAudio.PlayOneShot(mainMenuSFX[2]);
    }
    public void BackSF()
    {
        mainMenuAudio.PlayOneShot(mainMenuSFX[0]);
    }
    
    void Update()
    {
        BeatTimer();

        PlayMusic();
    }

    private void PlayMusic()
    {
        FirstOn();
    }

    private void BeatTimer()
    {
            if(timeTracker >= beatLength)
            {
                beatsTracker++;
                beatCount = true;

                if(defaultIsPlaying && beatsTracker >= 16)
                {
                    beatsTracker = 0;

                   
                }

                timeTracker = 0;
            }
            else
        {
            beatCount = false;
        }

            timeTracker += Time.deltaTime;
    }
    private void FirstOn()
    {
        if (isOn && !musicAudio.isPlaying && beatCount)
        {
            musicAudio.Play();
        }
    }
}
