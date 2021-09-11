using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Ins;

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

    public void PlayBackgroundMusic()
    {
        if (musicAus && backgroundMusic != null && backgroundMusic.Length > 0)
        {
            int ranIndex = Random.Range(0, backgroundMusic.Length);

            if (backgroundMusic[ranIndex])
            {
                musicAus.clip = backgroundMusic[ranIndex];
                musicAus.Play();
            }
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if (soundAus && sound)
        {
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
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
