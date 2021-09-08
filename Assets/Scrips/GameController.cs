using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;

public class GameController : MonoBehaviour
{
    public static GameController ins;
    public bool gameIsPause = false;
    public float score = 0;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        TimeController.ins.enabled = false;
        Invoke("Load", 2f);
    }

    void Load()
    {
        QuestionManager.ins.CreateQuestion();
        TimeController.ins.enabled = true;
    }

    public void GamePause()
    {
        if (gameIsPause == false)
        {
            Time.timeScale = 0f;
            gameIsPause = true;
        }
        else
        {
            Time.timeScale = 1f;
            gameIsPause = false;
        }
    }

    public void Replay() {
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1f; //Set lại timeScale = 1, để chạy lại game 
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
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
