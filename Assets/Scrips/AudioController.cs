using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController ins;

    [Range(0, 1)]
    public float soundVolume = 1f;
    [Range(0, 1)]
    public float musicVolume = 0.5f;

    public AudioSource musicAus;
    public AudioSource soundAus;

    public AudioClip[] backgroundMusic;
    public AudioClip rightSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    private void Awake()
    {
        MakeSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBackgroundMusic();
    }

    private void Update()
    {
        soundAus.volume = soundVolume;
        musicAus.volume = musicVolume;
    }

    public void PlayBackgroundMusic()
    {
        if (musicAus && backgroundMusic != null && backgroundMusic.Length > 0)
        {
            int ranIndex = Random.Range(0, backgroundMusic.Length);

            if (backgroundMusic[ranIndex])
            {
                musicAus.volume = musicVolume;
                musicAus.clip = backgroundMusic[ranIndex];
                musicAus.Play();
            }
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if (soundAus && sound)
        {
            soundAus.volume = soundVolume;
            soundAus.PlayOneShot(sound);
        }
    }

    public void StopMusic()
    {
        if (musicAus)
        {
            musicAus.Stop();
        }
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }

    public void PlayLoseSound()
    {
        PlaySound(loseSound);
    }

    public void PlayRightSound()
    {
        PlaySound(rightSound);
    }

    public void MakeSingleton()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
