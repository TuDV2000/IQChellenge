using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DialogOption : MonoBehaviour
{
    public static DialogOption Ins;

    private void Awake()
    {
        MakeSingleton();    
    }

    public void Show()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void DoneSetting()
    {
        gameObject.SetActive(false);
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
