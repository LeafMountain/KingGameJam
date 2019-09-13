using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager_ref;

    private EnemyManager enemyManagerRef;

    private AudioSource[] musicSources;

    [Header("Audio Sources")]
    public AudioSource mainMenuAudio;
    public AudioSource sfxAudio;
    public AudioSource musicAudio;


    [Header("Menu Sound")]
    public AudioClip[] mainMenuSFX;

    [Header("SFX")]
    public AudioClip[] SFX;

    public AudioClip[] onDamageSFX;

    [Header("MusicClips")]
    public AudioClip[] musicClips;
    public AudioClip menuClip;
    public AudioClip pauseMusic;
    [HideInInspector]
    public bool isOn;
    private bool defaultIsPlaying;
    //[HideInInspector]
    public bool beatCount;

    private float timeTracker;
    private int beatsTracker;

    private int lastBeat;
   
    private int enemyTrackToPlay;
    private AudioClip upComingClip;
    private AudioSource upComingMusicSource;

    private int trackCurrentlyPlaying;
    private AudioClip activeMusicClip;
    private AudioSource activeMusicSource;



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
        StartCoroutine("BeatTimer");

        enemyManagerRef = EnemyManager.GetInstance();
        musicSources = transform.GetChild(1).GetComponentsInChildren<AudioSource>();
    }
    public void StartPlayMusicTrack()
    {
        mainMenuAudio.Stop();

        isOn = true;

    }
    public void StartMenuMusic()
    {
        mainMenuAudio.Stop();

        mainMenuAudio.clip = menuClip;
        mainMenuAudio.Play();
    }
    public void StartPauseMusic()
    {
        mainMenuAudio.Stop();
        mainMenuAudio.clip = pauseMusic;
        mainMenuAudio.Play();
        mainMenuAudio.loop = true;
    }

    public void PauseMusicTrack()
    {
        isOn = false;
    }
    public void StopMusicTrack()
    {
        isOn = false;
        foreach (AudioSource source in musicSources)
        {
            source.volume = 0;
        }
    }

    public void ButtonSelect()
    {
        mainMenuAudio.PlayOneShot(mainMenuSFX[2]);
    }
    public void BackSF()
    {
        mainMenuAudio.PlayOneShot(mainMenuSFX[0]);
    }
    public void ExplosionSurfer()
    {

        sfxAudio.pitch = Random.Range(0.8f, 1.2f);
        sfxAudio.PlayOneShot(SFX[5]);
    }
    
    void Update()
    {
        if(lastBeat != beatsTracker)
        {
            lastBeat = beatsTracker;
            CheckBeat();
            beatCount = true;
        }
        else
        {
            beatCount = false;
        }
        

        PlayMusic();

        if (beatCount)
        {
            CheckEnemies();
            PrepTrack();

           
        }
        if (!musicSources[0].isPlaying && isOn)
        {
            musicSources[0].Play();
        }
        DJ();
    }

    public void OnDamage()
    {
        int randomint = Random.Range(0, onDamageSFX.Length);

        sfxAudio.pitch = Random.Range(0.8f, 1.2f);

        sfxAudio.PlayOneShot(onDamageSFX[randomint]);

        
    }

    private void PlayMusic()
    {
        
       
    }

    IEnumerator BeatTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(.59f);
            beatsTracker++;
        }
    }

    private void CheckBeat()
    {
      

        if (isOn)
        {
          


            if (defaultIsPlaying && beatsTracker >= 16)
            {
                beatsTracker = 0;

             

            }
            else if (!defaultIsPlaying && beatsTracker >= 8 && isOn)
            {
               

                for (int i = 0; i < musicSources.Length; i++)
                {
                    if (i != 0)
                    {
                        musicSources[i].Stop();
                        musicSources[i].Play();
                       
                    }
                }

                beatsTracker = 0;
            }

           
        }
    }
    private void FirstOn()
    {
        if (isOn && !musicAudio.isPlaying && beatCount)
        {
            // musicAudio.Play();

            beatsTracker = 0;

            for (int i = 0; i < musicSources.Length; i++)
            {
                if(i != 0)
                {
                    musicSources[i].Play();
                    musicSources[i].volume = 0f;
                }
                else
                {
                    musicSources[i].Play();
                    musicSources[i].volume = 1f;
                }
            }
        }
    }

    private void CheckEnemies()
    {
        int trackToPlay = 0;

        if (enemyManagerRef.enemiesInGame.Count != 0)
        {



            foreach (AIBase enemy in enemyManagerRef.enemiesInGame)
            {
                if ((int)enemy.myType > trackToPlay)
                {
                    trackToPlay = (int)enemy.myType;
                }
            }

            enemyTrackToPlay = trackToPlay + 2;

        }
        else
        {
            enemyTrackToPlay = 1;
        }
    }

    private void PrepTrack()
    {
        if(enemyTrackToPlay != trackCurrentlyPlaying && isOn)
        {
            defaultIsPlaying = false;

            switch (enemyTrackToPlay)
            {
                case 0:

                    defaultIsPlaying = true;
                    trackCurrentlyPlaying = 0;
                    if (!musicSources[0].isPlaying)
                    {
                        musicSources[0].Play();
                    }

                    break;

                case 1:

                    upComingMusicSource = musicSources[1];

                    if (musicSources[1].clip == null)
                    {
                        musicSources[1].clip = musicClips[6];
                    }

                    trackCurrentlyPlaying = 1;
                    break;

                case 2:

                    upComingMusicSource = musicSources[2];

                    if (musicSources[2].clip == null)
                    {
                        musicSources[2].clip = musicClips[9];
                    }

                    trackCurrentlyPlaying = 2;
                    break;

                case 3:

                    upComingMusicSource = musicSources[3];

                    if (musicSources[3].clip == null)
                    {
                        musicSources[3].clip = musicClips[8];
                    }

                    trackCurrentlyPlaying = 3;
                    break;

                case 4:

                    trackCurrentlyPlaying = 4;

                    if (musicSources[4].clip == null)
                    {
                        musicSources[4].clip = musicClips[5];
                    }

                    upComingMusicSource = musicSources[4];

                    break;

                case 5:

                    upComingMusicSource = musicSources[5];

                    if (musicSources[5].clip == null)
                    {
                        musicSources[5].clip = musicClips[2];
                    }

                    trackCurrentlyPlaying = 5;
                    break;

                case 6:

                    upComingMusicSource = musicSources[6];

                    if(musicSources[6].clip == null)
                    {
                        musicSources[6].clip = musicClips[0];
                    }

                    trackCurrentlyPlaying = 6;
                    break;
            }
        }
    }

    private void DJ()
    {
        if (isOn)
        {



            for (int i = 0; i < 7; i++)
            {
                if (i != enemyTrackToPlay - 1)
                {
                    musicSources[i].volume -= 0.1f;
                }
                else
                {
                    musicSources[i].volume += 0.1f;
                }
            }

        }
    }
}
