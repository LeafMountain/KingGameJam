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
        
    }
}
